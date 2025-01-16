using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using yogago.Models;

namespace yogago.Controllers
{
    public class HomeController :  BaseController
    {


        private readonly ModelContext _context;

       
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ModelContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {

            //desplay test
            //Userinfo ,member,testamoneal ==>LINQ

            var userinfo = _context.Userinfos.ToList();
            var test = _context.Testimonials.ToList();
            var member = _context.Members.ToList();

            var testinfo = from T in test
                           join M in member on T.Memberid equals M.Memberid
                           join U in userinfo on M.Userid equals U.Userid
                           select new TestInfo { Test = T, Member = M, Userinfo = U };



            return View(testinfo.ToList());

        }

        public IActionResult about()
        {
            return View();
        }

        public IActionResult price()
        {
            return View();
        }


        public IActionResult classs()
        {
            return View();
        }

        public IActionResult Trainers()
        {
            return View();
        }

        public IActionResult Payment()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

        //Git
        public async Task<IActionResult> Profiles()
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

        public IActionResult AddTest(Testimonial test)
        {


            //var result = SetSessionData();
            //if (result != null) return result; //  session retreve to
            //
            var username = HttpContext.Session.GetString("Username");
            var Userid = HttpContext.Session.GetInt32("Userid");
            var Rolename = HttpContext.Session.GetString("Rolename");
            var Roleid = HttpContext.Session.GetInt32("Roleid");

            

            if (string.IsNullOrEmpty(username) || Userid == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (ModelState.IsValid)
            {    //T=> Member id =>(m)userid=>(userinfo) userid

                var memberId = _context.Members
                .Where(m => m.Userid == Userid)
                .Select(m => m.Memberid)
                .FirstOrDefault();

                //Testimonial 
                test = new Testimonial
                {
                    Memberid = memberId,
                    Content = test.Content,
                    Rating = test.Rating,
                    Status = "Pending",
                    Submitteddate = DateTime.Now
                };
                _context.Testimonials.Add(test);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Your testimonial has been submitted and is awaiting approval from the Admin.";
                return RedirectToAction("Index", "Home"); 
            }

            TempData["ErrorMessage"] = "Please correct the errors and try again.";

            return View(test);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clear all session data
            return RedirectToAction("Login", "Login");
        }

    }
}
