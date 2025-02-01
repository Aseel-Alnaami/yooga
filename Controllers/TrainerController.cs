using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yogago.Models;

namespace yogago.Controllers
{
    public class TrainerController : Controller
    {
        
            private readonly ModelContext _context;

            public TrainerController(ModelContext context)
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
            var profileimg = HttpContext.Session.GetString("Profileimg");

            ViewBag.img = profileimg;

            return View();
        }

        
        //    //var UserPro = await _context.Userinfos.Where(x => x.Userid == Userid);
        //    ////for just one row (FirstOrDefaultAsync)
        //    //var UserPro = await _context.Userinfos.FirstOrDefaultAsync(x => x.Userid == Userid);

        public async Task<IActionResult> Profiles()
        {
            var username = HttpContext.Session.GetString("Username");
            var Userid = HttpContext.Session.GetInt32("Userid");

            if (string.IsNullOrEmpty(username) || Userid == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var profileimg = HttpContext.Session.GetString("Profileimg");

            ViewBag.img = profileimg;
            ViewBag.Username = username;

            var Trainerinfo = await _context.Userinfos
                .Include(x => x.Trainers) 
                .FirstOrDefaultAsync(x => x.Userid == Userid);

            if (Trainerinfo == null)
            {
                return NotFound("User information not found.");
            }

            return View(Trainerinfo); 
        }


        public IActionResult TClasses()
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

            return View();
        }
        public IActionResult Traineeslist()
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
            return View();
        }
    }
}
