using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Formats.Asn1;
using System.Linq;
using System.Numerics;
using yogago.Models;
using yogago.Services;

namespace yogago.Controllers
{
    public class HomeController :  BaseController
    {


        private readonly ModelContext _context;
        private readonly EmailService _emailService;


        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ModelContext context, EmailService emailService)
        {
            _logger = logger;
            _context = context;
            _emailService = emailService;

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
            ViewBag.Plans = _context.Plans.ToList(); // Replace _context.Plans with your data source



            //ViewBag.HomeTexts = _context.Homemanagementtexts.ToList();
            ViewBag.CenterName = _context.Homemanagementtexts.FirstOrDefault(t => t.Textname == "Centername");
            ViewBag.TitleHeader = _context.Homemanagementtexts.FirstOrDefault(t => t.Textname == "Titileinheader");
            ViewBag.DescInHeader = _context.Homemanagementtexts.FirstOrDefault(t => t.Textname == "Descinheader");
            ViewBag.AboutUs1 = _context.Homemanagementtexts.FirstOrDefault(t => t.Textname == "Aboutus1");
            ViewBag.AboutUs2 = _context.Homemanagementtexts.FirstOrDefault(t => t.Textname == "Aboutus2");
            ViewBag.Address = _context.Homemanagementtexts.FirstOrDefault(t => t.Textname == "Address");
            ViewBag.PhoneNumber = _context.Homemanagementtexts.FirstOrDefault(t => t.Textname == "Phnumber");
            ViewBag.Email = _context.Homemanagementtexts.FirstOrDefault(t => t.Textname == "Email");



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

		//Git

		public IActionResult classs(decimal? categoryId)
        {

            var classs = _context.Classes
                .Include(c => c.Category)
                .Include(c => c.Trainer)
                .ThenInclude(t => t.User)
                .ToList();

            if (categoryId.HasValue && categoryId != 0)
            {
                classs = classs.Where(c => c.Categoryid == categoryId.Value).ToList();
            }
            ViewBag.SelectedCategory = categoryId; // To highlight the selected category button
            ViewBag.Categories = _context.Classcategories.ToList(); // Pass all categories for the filter

            return View(classs);
        }
        ///*******************************Trainers***************************************/
        public IActionResult Trainers()
        {
            var trainers = _context.Trainers.Include(t => t.User).ToList();

            return View(trainers);
        }

        //Git
        //public IActionResult TClass(decimal trainerid, string trainername, decimal? categoryId)
        //{

        //    // trainer classes
        //    var tclass = _context.Classes
        //        .Include(c => c.Category)
        //        .Include(c => c.Trainer)
        //        .ThenInclude(t => t.User)
        //        .Include(c => c.Classmembers)
        //        .Where(c => c.Trainerid == trainerid)
        //        .ToList();
        //    if (categoryId.HasValue && categoryId != 0)
        //    {
        //        tclass = tclass.Where(c => c.Categoryid == categoryId.Value).ToList();
        //    }

        //    // Pass data to the view
        //    ViewBag.TrainerName = trainername;
        //    ViewBag.TrainerId = trainerid; // To maintain filter links in the view
        //    ViewBag.SelectedCategory = categoryId; // Pass the selected category ID for highlighting
        //    ViewBag.Categories = _context.Classcategories.ToList(); // Pass categories for dynamic filter links

        //    return View(tclass);
        

        public IActionResult TClass(decimal trainerid, string trainername, decimal? categoryId)
{
    // Fetch all classes for the given trainer
    var query = _context.Classes
        .Include(c => c.Category) // Ensure the category is included
        .Include(c => c.Trainer)
        .ThenInclude(t => t.User)
        .Include(c => c.Classmembers)
        .Where(c => c.Trainerid == trainerid);

    // Apply filtering if categoryId is provided and not zero
    if (categoryId.HasValue && categoryId != 0)
    {
        query = query.Where(c => c.Categoryid == categoryId.Value);
    }

    var tclass = query.ToList();

    // Pass data to the view
    ViewBag.TrainerName = trainername;
    ViewBag.TrainerId = trainerid; // Pass TrainerId for dynamic links
    ViewBag.SelectedCategory = categoryId; // Pass the selected category ID
    ViewBag.Categories = _context.Classcategories.ToList(); // Pass all categories

    return View(tclass);
}


            //if (categoryId.HasValue)
            //{
            //    tclass = tclass.Where(c => c.Categoryid == categoryId.Value).ToList();
            //}
            //else if (categoryId == 1){
            //    tclass = tclass.Where(c => c.Categoryid == categoryId.Value).ToList();

            //}
            //else if (categoryId == 5)
            //{
            //    tclass = tclass.Where(c => c.Categoryid == categoryId.Value).ToList();

            //}
            //else if (categoryId == 6)
            //{
            //    tclass = tclass.Where(c => c.Categoryid == categoryId.Value).ToList();

            //}
            //else if (categoryId == 7)
            //{
            //    tclass = tclass.Where(c => c.Categoryid == categoryId.Value).ToList();

            //}
            //else if (categoryId == 8)
            //{
            //    tclass = tclass.Where(c => c.Categoryid == categoryId.Value).ToList();

            //}
            //else if (categoryId == 9)
            //{
            //    tclass = tclass.Where(c => c.Categoryid == categoryId.Value).ToList();

            //}else if (categoryId == 0){
            //    tclass = tclass.Where(c => c.Categoryid == categoryId.Value).ToList();

            //}
            //ViewBag.TrainerName = trainername;
            //    ViewBag.Categories = _context.Classcategories.ToList(); // Pass all categories for filtering

            //    return View(tclass);
            //}



            [HttpPost]
        public IActionResult Addclass(Classmember classmember ,decimal classid)
        {
            var username = HttpContext.Session.GetString("Username");
            var Userid = HttpContext.Session.GetInt32("Userid");


            if (string.IsNullOrEmpty(username) || Userid == null)
            {
                return RedirectToAction("Login", "Login");
            }

            ViewBag.Username = username;
            var member=_context.Members.FirstOrDefault(x=>x.Userid == Userid);

            //if (member == null)
            //{
            //    return BadRequest("No member found for the provided user ID.");
            //}

            classmember = new Classmember
            {
                Classid = classid,
                Memberid = member.Memberid


            };
            var exists = _context.Classmembers.Any(cm => cm.Classid == classid && cm.Memberid == Userid);
            if (!exists)
            {
                _context.Classmembers.Add(classmember);
                _context.SaveChanges();

                // Store a success message in TempData
                TempData["SuccessMessage"] = "Class successfully added!";
            }
            else
            {
                TempData["ErrorMessage"] = "You have already added this class.";
            }

            return RedirectToAction("TClass");
        }

           





        //Git

        public IActionResult Payment(decimal planid)
        {
            var username = HttpContext.Session.GetString("Username");
            var Userid = HttpContext.Session.GetInt32("Userid");
           

            if (string.IsNullOrEmpty(username) || Userid == null)
            {
                return RedirectToAction("Login", "Login");
            }

            ViewBag.Username = username;

            var plan = _context.Plans.FirstOrDefault(p => p.Planid == planid);
            if (plan == null)
            {
                TempData["ErrorMessage"] = "Invalid plan selected.";
                return RedirectToAction("Index");
            }

            ViewBag.Plan = plan;
            ViewBag.Username = username;

            return View();
        }


        [HttpPost]
        public IActionResult Payment(Paymentinfo pay,decimal planid)
        {
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Login");
            }



            // Retrieve the plan info

            var plan = _context.Plans.FirstOrDefault(p => p.Planid == planid);
            if (plan == null)
            {

                TempData["ErrorMessage"] = "Invalid plan selected.";
                return RedirectToAction("Index");
            }

            Debug.WriteLine($"Plan found: {plan.Planname}, Price: {plan.Price}");

            //try
            //{
            // Retrieve and compare CLOB values using substring
            var paymentInfo = _context.Paymentinfos
                    .AsEnumerable() // Fetch data into memory to handle CLOB comparison
                    .FirstOrDefault(p =>
                        p.Fullname == pay.Fullname &&
                        p.Email == pay.Email &&
                        p.Zipcode == pay.Zipcode &&
                        p.Cardnumberencrypted.Substring(0, 16) == pay.Cardnumberencrypted.Substring(0, 16) && // Compare first 16 chars
                        p.Cvvencrypted == pay.Cvvencrypted &&
                        p.Expirymonth == pay.Expirymonth 
                        //&&p.Expiryyear == pay.Expiryyear
                    );
            if (paymentInfo == null)
            {
                TempData["ErrorMessage"] = "Invalid payment details.";
                return RedirectToAction("Payment", new { planid });
            }


            if (paymentInfo.Expiryyear < plan.Price)
            {
                TempData["ErrorMessage"] = "Insufficient balance.";
                return RedirectToAction("Payment", new { planid });
            }

            TempData["PlanId"] = plan.Planid.ToString();
            TempData["CardId"] = paymentInfo.Cardid.ToString();
            TempData["Balance"] = paymentInfo.Expiryyear.ToString();


            return RedirectToAction("Invoice");

        }


        public IActionResult Invoice()
        {
            var username = HttpContext.Session.GetString("Username");
            var Userid = HttpContext.Session.GetInt32("Userid");


            if (string.IsNullOrEmpty(username) || Userid == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var planId = decimal.Parse(TempData["PlanId"].ToString());
            var cardId = decimal.Parse(TempData["CardId"].ToString());
            var balance = int.Parse(TempData["Balance"].ToString());

            var plan = _context.Plans.FirstOrDefault(p => p.Planid == planId);
            var paymentInfo = _context.Paymentinfos.FirstOrDefault(p => p.Cardid == cardId);

            ViewBag.Plan = plan;
            ViewBag.PaymentInfo = paymentInfo;
            ViewBag.Balance = balance;

            return View();
        }






        [HttpPost]
        public async Task<IActionResult> ConfirmPurchase(decimal planId, decimal cardId, [FromServices] EmailService emailService)
        {
            try
            {
                // Retrieve the plan details
                var plan = _context.Plans.FirstOrDefault(p => p.Planid == planId);
                if (plan == null)
                {
                    TempData["ErrorMessage"] = "Plan not found.";
                    return RedirectToAction("Index");
                }

                // Retrieve payment info
                var paymentInfo = _context.Paymentinfos.FirstOrDefault(p => p.Cardid == cardId);
                if (paymentInfo == null)
                {
                    TempData["ErrorMessage"] = "Payment details not found.";
                    return RedirectToAction("Index");
                }

                // Check if the user has sufficient balance
                if (paymentInfo.Expiryyear < plan.Price)
                {
                    TempData["ErrorMessage"] = "Insufficient balance.";
                    return RedirectToAction("Index");
                }

                // Deduct the price from the balance
                paymentInfo.Expiryyear -= (int)plan.Price;

                // Save the plan in the Memberplan table
                var userId = HttpContext.Session.GetInt32("Userid");
                var member = _context.Members.FirstOrDefault(m => m.Userid == userId);
                if (member == null)
                {
                    TempData["ErrorMessage"] = "Member not found.";
                    return RedirectToAction("Index");
                }

                var memberPlan = new Memberplan
                {
                    Memberid = member.Memberid,
                    Planid = plan.Planid,
                    Startdate = DateTime.Now
                };
                _context.Memberplans.Add(memberPlan);
                _context.SaveChanges(); // Save the Memberplan and generate Memberplanid

                Debug.WriteLine($"Memberplan ID: {memberPlan.Memberplanid}");

                // Create and save the invoice
                //var invoice = new Invoiceinfo
                //{
                //    Memberplanid = memberPlan.Memberplanid, // Associate with the saved Memberplan
                //    Amount = plan.Price,
                //    Generateddate = DateTime.Now,
                //    Emailsent = false
                //};
                //_context.Invoiceinfos.Add(invoice);
                //_context.SaveChanges(); // Save the Invoiceinfo

                string emailadd = paymentInfo.Email;
                string subject = "Payment Successful!";
                string body = $@"  <h1>Thank you for your purchase!</h1><p>You have successfully purchased the plan. We hope you enjoy your journey with us.</p>";
                //await emailService.SendEmailAsync(emailadd, subject, body);
                await _emailService.SendEmailAsync(emailadd, subject, body);
                //await emailService.SendEmailAsync(emailadd, subject, body);

                TempData["SuccessMessage"] = "Purchase completed successfully! A confirmation email has been sent.";
                return RedirectToAction("Index");


            }
            catch (Exception ex)
            {
                // Log the error for debugging
                Console.WriteLine($"Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }

                TempData["ErrorMessage"] = "An error occurred while processing your request.";
                return RedirectToAction("Index");
            }
        }


        //[HttpPost]
        //public IActionResult ConfirmPurchase(decimal planId, decimal cardId)
        //{
        //    try
        //    {
        //        // Retrieve the plan details
        //        var plan = _context.Plans.FirstOrDefault(p => p.Planid == planId);
        //        if (plan == null)
        //        {
        //            TempData["ErrorMessage"] = "Plan not found.";
        //            return RedirectToAction("Index");
        //        }

        //        // Retrieve payment info
        //        var paymentInfo = _context.Paymentinfos.FirstOrDefault(p => p.Cardid == cardId);
        //        if (paymentInfo == null)
        //        {
        //            TempData["ErrorMessage"] = "Payment details not found.";
        //            return RedirectToAction("Index");
        //        }

        //        // Check if the user has sufficient balance
        //        if (paymentInfo.Expiryyear < plan.Price)
        //        {
        //            TempData["ErrorMessage"] = "Insufficient balance.";
        //            return RedirectToAction("Index");
        //        }

        //        // Deduct the price from the balance
        //        paymentInfo.Expiryyear -= (int)plan.Price;

        //        // Save the plan in the Memberplan table
        //        var userId = HttpContext.Session.GetInt32("Userid");
        //        var member = _context.Members.FirstOrDefault(m => m.Userid == userId);
        //        if (member == null)
        //        {
        //            TempData["ErrorMessage"] = "Member not found.";
        //            return RedirectToAction("Index");
        //        }

        //        var memberPlan = new Memberplan
        //        {
        //            Memberid = member.Memberid,
        //            Planid = plan.Planid,
        //            Startdate = DateTime.Now
        //        };
        //       _context.Memberplans.Add(memberPlan);
        //        _context.SaveChanges(); // Save the Memberplan and generate Memberplanid

        //        Debug.WriteLine($"Memberplan ID: {memberPlan.Memberplanid}");

        //        // Create and save the invoice
        //        var invoice = new Invoiceinfo
        //        {
        //            Memberplanid = memberPlan.Memberplanid, // Associate with the saved Memberplan
        //            Amount = plan.Price,
        //            Generateddate = DateTime.Now,
        //            Emailsent = false
        //        };
        //        _context.Invoiceinfos.Add(invoice);
        //        _context.SaveChanges(); // Save the Invoiceinfo

        //        string emailadd = paymentInfo.Email;
        //        string subject = "Payment Successful!";
        //        string body = $@"
        //    <h1>Thank you for your purchase!</h1>
        //    <p>You have successfully purchased the plan. We hope you enjoy your journey with us.</p>";
        //        await emailService.SendEmailAsync(emailadd, subject, body);

        //        TempData["SuccessMessage"] = "Purchase completed successfully! A confirmation email has been sent.";
        //        return RedirectToAction("Index");


        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the error for debugging
        //        Console.WriteLine($"Error: {ex.Message}");
        //        if (ex.InnerException != null)
        //        {
        //            Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
        //        }

        //        TempData["ErrorMessage"] = "An error occurred while processing your request.";
        //        return RedirectToAction("Index");
        //    }
        //}



           //Git
        public IActionResult Contact()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult Contact([Bind("Name,Email,Subject,Message")]  Contactu contactu)
        //{

        //    ////contactu = new Contactu
        //    ////{
        //    ////    Name = name,
        //    ////    Email = email,
        //    ////    Subject = subject,
        //    ////    Message = Message



        //    ////}; her we dont have to creat new inestanes bes 
        //    //// no Need to Transform Data
        //    //// no Combining Data from Multiple Sources:like time now
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {

        //            contactu.Submitteddate = DateTime.Now;

        //            _context.Contactus.Add(contactu);
        //            _context.SaveChanges();

        //            TempData["SuccessMessage"] = "Your message has been sent successfully!";
        //            return RedirectToAction("Contact");
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"Error saving to database: {ex.Message}");

        //            TempData["ErrorMessage"] = "Failed to save your message. Please try again.";
        //        }

        //    }
        //    else
        //    {
        //        TempData["ErrorMessage"] = "Please correct the errors and try again.";
        //    }

        //    // Return the form with validation errors
        //    return View(contactu);
        //}


        [HttpPost]
        public IActionResult Contact(Contactu contactu)
        {
            if (ModelState.IsValid)
            {
                contactu.Submitteddate = DateTime.Now; // Add current timestamp
                _context.Contactus.Add(contactu); // Save to database
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Your message has been sent successfully!";
                return RedirectToAction("Contact");
            }

            TempData["ErrorMessage"] = "Please correct the errors and try again.";
            return View(contactu); // Return the form with validation errors
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
        public async Task<IActionResult> Plans( )
        {

           //plan= _context.Plans != null ?
           //               View(await _context.Plans.ToListAsync()) :
           //               Problem("Entity set 'ModelContext.Plans'  is null.");


            if (_context.Plans == null)
            {

                return Problem("Entity set 'ModelContext.Plans' is null.");
            }

            ViewBag.Plans = await _context.Plans.ToListAsync();

            var plan = await _context.Plans.ToListAsync();
            return View(plan);
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
