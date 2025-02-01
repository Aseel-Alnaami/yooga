using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using yogago.Models;
using yogago.Services;


namespace yogago.Controllers
{
    public class AdminController : BaseController
    {
		private readonly ModelContext _context;
        private readonly EmailService _emailService;

        public AdminController(ModelContext context, EmailService emailService)
		{
			_context = context;
            _emailService = emailService;

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
            ViewBag.Userid = Userid;

            var profileimg = HttpContext.Session.GetString("Profileimg");
            
            ViewBag.img = profileimg;
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
        public async Task<IActionResult> ProfileEdit(Class2 class2, decimal id)
        {
            if (id == null || id != class2.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingUser = await _context.Userinfos.FindAsync(id);
                    if (existingUser == null)
                    {
                        return NotFound();
                    }

                    // Update user fields
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
                        return NotFound();
                    }
                    throw;
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

            var profileimg = HttpContext.Session.GetString("Profileimg");

            ViewBag.img = profileimg;

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






        //Git
        public async Task<IActionResult> Subsecrebtions()
        {




			var username = HttpContext.Session.GetString("Username");
			var profileimg = HttpContext.Session.GetString("Profileimg");
			if (string.IsNullOrEmpty(username))
			{
				return RedirectToAction("Login", "Login");
			}
			ViewBag.Username = username;
			ViewBag.img = profileimg;

			//// to desplay the number of trainer and member 

			ViewBag.TrainersSUM = _context.Userinfos.Where(u => u.Roleid == 2).Count();
			ViewBag.MembersSUM = _context.Userinfos.Where(u => u.Roleid == 3).Count();
			ViewBag.PlansSUM = _context.Plans.Count();
			ViewBag.ClassesSUM = _context.Classes.Count();
           
            //Usesinfo,Member,plan,mmberplan

            ViewBag.img = profileimg;
            var memberpplan = _context.Memberplans.ToList();
            var plan = _context.Plans.ToList();
            var member = _context.Members.ToList();
            var userinfo = _context.Userinfos.ToList();


            var sub = from Mp in memberpplan
                      join P in plan on Mp.Planid equals P.Planid
                      join M in member on Mp.Memberid equals M.Memberid
                      join U in userinfo on M.Userid equals U.Userid
                      select new Subsecrebtions { Memberplan=Mp, Member=M, Userinfo=U,Plan=P};


            return View(sub);
        }

        [HttpPost]

        public async Task<IActionResult> Subsecrebtions(DateTime? startDate, DateTime? endDate)

        {
			var memberpplan = _context.Memberplans.ToList();
			var plan = _context.Plans.ToList();
			var member = _context.Members.ToList();
			var userinfo = _context.Userinfos.ToList();

            

            var sub = from Mp in memberpplan
					  join P in plan on Mp.Planid equals P.Planid
					  join M in member on Mp.Memberid equals M.Memberid
					  join U in userinfo on M.Userid equals U.Userid
					  select new Subsecrebtions { Memberplan = Mp, Member = M, Userinfo = U, Plan = P };

			//for  chart 
			var planUser = from Mp in memberpplan
						   join P in plan on Mp.Planid equals P.Planid
						   group Mp by P.Planname into grouped
						   select new
						   {
							   PlanName = grouped.Key,
							   MemberCount = grouped.Count()
						   };

			ViewBag.Y = planUser.Select(x => x.PlanName).ToList();
			ViewBag.X = planUser.Select(x => x.MemberCount).ToList();

			//for table

			if (startDate == null && endDate == null)

            {

                return View(sub);
            }
            else if (endDate == null&&startDate!=null) {
            var result= from Mp in memberpplan
						join P in plan on Mp.Planid equals P.Planid
						join M in member on Mp.Memberid equals M.Memberid
						join U in userinfo on M.Userid equals U.Userid
                        where Mp.Startdate>=startDate
						orderby Mp.Startdate descending 
						select new Subsecrebtions { Memberplan = Mp, Member = M, Userinfo = U, Plan = P };

                return View(result);
			}

			else if (startDate == null&& endDate!=null) {
				var result = from Mp in memberpplan
							 join P in plan on Mp.Planid equals P.Planid
							 join M in member on Mp.Memberid equals M.Memberid
							 join U in userinfo on M.Userid equals U.Userid
							 where Mp.Startdate <= endDate
							 orderby Mp.Startdate descending 
							 select new Subsecrebtions { Memberplan = Mp, Member = M, Userinfo = U, Plan = P };

				return View(result);

			}
            else {
                var result= from Mp in memberpplan
						join P in plan on Mp.Planid equals P.Planid
						join M in member on Mp.Memberid equals M.Memberid
						join U in userinfo on M.Userid equals U.Userid
                        where Mp.Startdate >= startDate && Mp.Startdate <= endDate
						orderby Mp.Startdate ascending 
                        select new Subsecrebtions { Memberplan = Mp, Member = M, Userinfo = U, Plan = P };

                return View(result);
            }


        }


        ///************************ContactUS*******************
        //git
        public IActionResult Contact()
        {

            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Login");
            }
            ViewBag.Username = username;
            var profileimg = HttpContext.Session.GetString("Profileimg");

            ViewBag.img = profileimg;

            var contacts = _context.Contactus.ToList();
            var model = contacts.Select(c => new contactview
            {
                Contact = c,
                Response = string.Empty
            }).ToList();

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> ContactAsync(decimal contactId, string response)
        {
            if (string.IsNullOrWhiteSpace(response))
            {
                TempData["ErrorMessage"] = "Response cannot be empty.";
                return RedirectToAction("Contact");
            }

            var contact = await _context.Contactus.FindAsync(contactId);

            if (contact == null)
            {
                TempData["ErrorMessage"] = "Contact not found.";
                return RedirectToAction("Contact");
            }
            //SEND THE EMAIIIL....
            try
            {
                string subject = $"{contact.Subject} - Response";

                await _emailService.SendEmailAsync(contact.Email, subject, response);

                TempData["SuccessMessage"] = "Email sent successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error sending email: {ex.Message}";
            }

            return RedirectToAction("Contact");
        }


        //git
        public IActionResult Homemanagement()
        {
            return View();
                }


            ///******************Reports***********
            // git
            //    public IActionResult Reports(DateTime? startDate, DateTime? endDate)
            //{
            //    var profileimg = HttpContext.Session.GetString("Profileimg");

            //    ViewBag.img = profileimg;
            //Triner => class=> catofclass =>Userinfo R4
            //Member => memberplan => plan => Userinfo R3
            //Monthly New members => in month ,member => memberplan => plan => Userinfo R2
            //Monthly revenue =>  invoice => plan  R1
            //Userinfo + triner +class +catclass +member +memberplan +plan +invoice ==8


            //var userinfo = _context.Userinfos.ToList();
            //var member=_context.Members.ToList();
            //var trainer=_context.Trainers.ToList();
            //var plan=_context.Plans.ToList();
            //var memberplan=_context.Memberplans.Include(x=>x.Memberid).Include(u=>u.) ToList();
            //var classes=_context.Classes.ToList();
            //var cat=_context.Classcategories.ToList();
            //R1 same 2

            //var memberpplan = _context.Memberplans.ToList();
            //var plan = _context.Plans.ToList();
            //var member = _context.Members.ToList();
            //var userinfo = _context.Userinfos.ToList();


            //var Profits =_context.Memberplans.Include(mp=>mp.Plan).Include(mp=>mp.Member).ThenInclude(m=>m.User)
            //    .Where(mp => mp.Startdate >= startDate && mp.Startdate <= endDate)
            //    .Select(mp => new
            //    {
            //        FullName = mp.Member.User.Fullname,
            //        PlanName = mp.Plan.Planname,
            //        PlanPrice = mp.Plan.Price,
            //        StartDate = mp.Startdate
            //    })
            //    .ToList();

            //R2



            //     var Subsecrebtions = from Mp in memberpplan
            //join P in plan on Mp.Planid equals P.Planid
            //join M in member on Mp.Memberid equals M.Memberid
            //join U in userinfo on M.Userid equals U.Userid
            //select new Subsecrebtions { Memberplan = Mp, Member = M, Userinfo = U, Plan = P };

            //     //R3
            //     var Profits = _context.Memberplans.Include(m => m.Plan).Include(m => m.Member).ThenInclude(u => u.User).ToList();

            //     //var classinfo = _context.Classes.Include(t => t.Category).Include(t => t.Trainer).ThenInclude(u => u.User).ToList();
            //     //*************
            //     //var reportData = Tuple.Create<IEnumerable<Subsecrebtions>, IEnumerable<Profits>, IEnumerable<classinfo>>(Subsecrebtions, Profits, classinfo);
            //     //var reportData = Tuple.Create<IEnumerable<Subsecrebtions>, IEnumerable<plan>, IEnumerable<classinfo>>(Subsecrebtions, Profits, classinfo);

            //     var totalprice =Subsecrebtions.Sum(x=>x.Plan.Price);
            //     ViewBag.Totalprice = totalprice;


            //     var totalRecords = Subsecrebtions.Count(); // Total records
            //     int filteredCount;

            //     if (startDate == null && endDate == null)
            //     {
            //         filteredCount = totalRecords;
            //     }
            //     else if (endDate == null && startDate != null)
            //     {
            //         filteredCount = Subsecrebtions.Count(s => s.Memberplan.Startdate >= startDate);
            //     }
            //     else if (startDate == null && endDate != null)
            //     {
            //         filteredCount = Subsecrebtions.Count(s => s.Memberplan.Startdate <= endDate);
            //     }
            //     else
            //     {
            //         filteredCount = Subsecrebtions.Count(s => s.Memberplan.Startdate >= startDate && s.Memberplan.Startdate <= endDate);
            //     }

            //     // Calculate percentage
            //     double percentage = ((double)filteredCount / totalRecords) * 100;









            //     return View(Subsecrebtions);
            //         }

            public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clear all session data
            return RedirectToAction("Login", "Login");
        }
    }
}
