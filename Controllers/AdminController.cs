using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using yogago.Models;


namespace yogago.Controllers
{
    public class AdminController : BaseController
    {
		private readonly ModelContext _context;

		public AdminController(ModelContext context)
		{
			_context = context;
		}
		public IActionResult Index()
        {
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Login");
            }
            ViewBag.Username = username;


            //// to desplay the number of trainer and member 

            ViewBag.TrainersSUM = _context.Userinfos.Where(u => u.Roleid == 2).Count();
            ViewBag.MembersSUM = _context.Userinfos.Where(u => u.Roleid == 3).Count();
            ViewBag.PlansSUM = _context.Plans.Count();
            ViewBag.ClassesSUM = _context.Classes.Count();


            return View();
		}
        //Git
        public async Task<IActionResult> ProfilesAsync()
        {
            var username = HttpContext.Session.GetString("Username");
            var Userid = HttpContext.Session.GetInt32("Userid");
            var Rolename = HttpContext.Session.GetString("Rolename");
            var Roleid = HttpContext.Session.GetInt32("Roleid");


            if (string.IsNullOrEmpty(username) || Userid == null)
            {
                return RedirectToAction("Login", "Login");
            }
            ViewBag.Username = username;
            ViewBag.Rolename = Rolename;

            //await UserPro.ToListAsync()
            //var UserPro = await _context.Userinfos.Where(x => x.Userid == Userid);
            //for just one row (FirstOrDefaultAsync)
            var UserPro = await _context.Userinfos.FirstOrDefaultAsync(x => x.Userid == Userid);

            return View(UserPro);



        }
        public async Task<IActionResult> ProfileEdit()
        {
            var SUserid = HttpContext.Session.GetInt32("Userid");
            if (SUserid == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var userinfo = await _context.Userinfos.Include(u => u.Role).FirstOrDefaultAsync(u => u.Userid == SUserid);
            if (userinfo == null)
            {
                return NotFound();
            }

            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "RoleName", userinfo.Roleid);
            return View(userinfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProfileEdit(Class2 class2)
        {
            var SUserid = HttpContext.Session.GetInt32("Userid");
            if (SUserid == null || SUserid != class2.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingUser = await _context.Userinfos.FindAsync(SUserid);
                    if (existingUser == null)
                    {
                        return NotFound();
                    }

                    // Map fields from Class2 to Userinfo
                    existingUser.Fullname = class2.Fullname;
                    existingUser.Username = class2.Username;
                    existingUser.Email = class2.Email;
                    existingUser.Phone = class2.Phone;
                    existingUser.Dateofbirth = class2.Dateofbirth;
                    existingUser.Address = class2.Address;
                    existingUser.Password = class2.Password;
                    existingUser.Roleid = class2.Roleid;

                    // Handle profile picture update
                    if (class2.Profilepicturefile != null)
                    {
                        string fileName = DateTime.Now.Ticks + "_" + Path.GetFileName(class2.Profilepicturefile.FileName);
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image", fileName);

                        if (!Directory.Exists(Path.GetDirectoryName(filePath)!))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
                        }

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await class2.Profilepicturefile.CopyToAsync(stream);
                        }

                        existingUser.Profilepicture = "/image/" + fileName;
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserinfoExists(class2.Userid))
                    {
                        return NotFound(); // User no longer exists
                    }
                    throw; // Re-throw the exception for unexpected issues
                }

                return RedirectToAction(nameof(ProfileEdit));
            }

            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "RoleName", class2.Roleid);
            return View(class2);
        }

        private bool UserinfoExists(decimal userid)
        {
            return _context.Userinfos.Any(e => e.Userid == userid);
        }






        public IActionResult Test()
        {

            //var result = SetSessionData();
            //if (result != null) return result; //  session retreve t
            //ViewBag.Username = username;

            var username = HttpContext.Session.GetString("Username");
            var Userid = HttpContext.Session.GetInt32("Userid");
            var Rolename = HttpContext.Session.GetString("Rolename");
            var Roleid = HttpContext.Session.GetInt32("Roleid");


            if (string.IsNullOrEmpty(username) || Userid == null)
            {
                return RedirectToAction("Login", "Login");
            }
            ViewBag.Rolename = Rolename;
            ViewBag.Username = username;
            ViewBag.Userid = Userid;



            //userinfo(IMG  , Fullname ),test tabel (contacttest ,  rating , date , statatus("pending"))

            //var testinfo = _context.Testimonials.Include(x => x.Member).Include(x => x.Userinfo).ToList();
            var userinfo=_context.Userinfos.ToList();
            var test=_context.Testimonials.ToList();
            var member =_context.Members.ToList();

            //LINQ
            var testinfo = from T in test
                           join M in member on T.Memberid equals M.Memberid
                           join U in userinfo on M.Userid equals U.Userid
                           select new TestInfo { Test = T, Member=M, Userinfo=U };
                          


            return View(testinfo.ToList());
        }




        [HttpPost]
        public IActionResult DeleteTest(int id)
        {
            var testimonial = _context.Testimonials.FirstOrDefault(t => t.Testimonialid == id);
            if (testimonial == null)
            {
                TempData["ErrorMessage"] = "Testimonial not found.";
                return RedirectToAction("Test"); 
            }

            _context.Testimonials.Remove(testimonial);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Testimonial deleted successfully.";
            return RedirectToAction("Test"); 
        }

        [HttpPost]

        public IActionResult ApprovalTest(int id)
        {
            var testimonial = _context.Testimonials.FirstOrDefault(t => t.Testimonialid == id);
            testimonial.Status = "Approved";
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Testimonial approved successfully.";
            return RedirectToAction("Test");
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clear all session data
            return RedirectToAction("Login", "Login");
        }
    }
}
