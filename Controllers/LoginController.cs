using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using yogago.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;


namespace yogago.Controllers
{
	public class LoginController : Controller
	{
		
		
			private readonly ModelContext _context;
			public LoginController(ModelContext context)
			{
				_context = context;
			}


			//GET
			public IActionResult Login()
			{
				return View();
			}





        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(Userlogin userlogin)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        ModelState.AddModelError("", "Invalid input.");
        //        return View(userlogin);
        //    }

        //    // Retrieve user from the database
        //    var user = await _context.Userlogins
        //        .Where(x => x.Username == userlogin.Username)
        //        .SingleOrDefaultAsync();

        //    if (user != null)
        //    {
        //        // Verify password (if using plain text for now)
        //        if (user.Password != userlogin.Password)
        //        {
        //            ModelState.AddModelError("", "Invalid password.");
        //            return View(userlogin);
        //        }

        //        // Store user info in session
        //        HttpContext.Session.SetInt32("Userid", (int)user.Userid);
        //        HttpContext.Session.SetString("Username", user.Username);
        //        HttpContext.Session.SetString("Password", user.Password);
        //        HttpContext.Session.SetInt32("Roleid", (int)user.Roleid);

        //        // Redirect based on role
        //        switch (user.Roleid)
        //        {
        //            case 1: // Admin
        //                return RedirectToAction("Index", "Admin");

        //            case 2: // Trainer
        //                return RedirectToAction("Index", "Trainer");

        //            case 3: // Customer
        //                return RedirectToAction("Index", "Home");

        //            default:
        //                ModelState.AddModelError("", "Unauthorized access.");
        //                return View(userlogin);
        //        }
        //    }

        //    // Handle invalid login
        //    ModelState.AddModelError("", "Invalid username or password.");
        //    return View(userlogin);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Loginview loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }

            var user = await _context.Userinfos
                .SingleOrDefaultAsync(x => x.Username == loginModel.Username);

            if (user != null)
            {
                // Verify password (assuming hashing is applied)
                if (user.Password != loginModel.Password)
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                    return View(loginModel);
                }

                HttpContext.Session.SetInt32("Userid", (int)user.Userid);
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetInt32("Roleid", (int)user.Roleid);

                return user.Roleid switch
                {
                    1 => RedirectToAction("Index", "Admin"),
                    2 => RedirectToAction("Index", "Trainer"),
                    3 => RedirectToAction("Index", "Home"),
                    _ => Unauthorized()
                };
            }

            ModelState.AddModelError("", "Invalid username or password.");
            return View(loginModel);
        }



        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clear all session data
            return RedirectToAction("Login", "Login");
        }


        public IActionResult Signup()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Signup(Class2 class2)
        {
            // Check if username already exists
            var existingUser = _context.Userlogins.SingleOrDefault(x => x.Username == class2.Username);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "Username already exists.");
                return View(class2);
            }

            if (ModelState.IsValid)
            {
                // Map Class2 to Userinfo
                var userInfo = new Userinfo
                {
                    Fullname = class2.Fullname,
                    Username = class2.Username,
                    Email = class2.Email,
                    Phone = class2.Phone,
                    Dateofbirth = class2.Dateofbirth,
                    Address = class2.Address,
                    Password = class2.Password, // Ideally, hash the password here
                    Roleid = 3, // Assuming "2" is the role ID for regular users
                    Dateadded = DateTime.Now
                };

                if (class2.Profilepicturefile != null)
                {
                    // Save Profile Picture (Example: Save to "wwwroot/uploads")
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + class2.Profilepicturefile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        class2.Profilepicturefile.CopyTo(fileStream);
                    }
                    userInfo.Profilepicture = uniqueFileName; // Store filename in DB
                }

                _context.Userinfos.Add(userInfo);
                _context.SaveChanges();

                // Create Userlogin record
                var userLogin = new Userlogin
                {
                    Username = class2.Username,
                    Password = class2.Password, // Same here, hash the password
                    Userid = userInfo.Userid,
                    Roleid = userInfo.Roleid
                };

                _context.Userlogins.Add(userLogin);
                _context.SaveChanges();

                var member = new Member
                {
                    Userid = userInfo.Userid,
                    Joindate = DateTime.Now 
                };

                _context.Members.Add(member);
                _context.SaveChanges();

                return RedirectToAction("Login", "Login"); 
            }

            return View(class2);
        }
    }


}











			//[HttpGet]
			//[HttpGet]
			//public IActionResult Signup()
			//{
			//	// Render the Signup view
			//	return View();
			//}
		

//        [HttpPost]
//        public async Task<IActionResult> Signup([Bind("Userid,Fullname,Username,Email,Phone,Dateofbirth,Address,Password")] Userinfo userinfo)
//        {
//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    // Assign default RoleID and set DateAdded
//                    userinfo.Roleid = 3; // Default role for new users
//                    userinfo.Dateadded = DateTime.Now;

//                    // Save Userinfo to the database
//                    _context.Userinfos.Add(userinfo);
//                    await _context.SaveChangesAsync();

//                    // Create a UserLogin entry
//                    Userlogin userLogin = new Userlogin
//                    {
//                        Username = userinfo.Username,
//                        Password = userinfo.Password, // Save plain-text password
//                        Roleid = userinfo.Roleid,
//                        Userid = userinfo.Userid // Link to the Userinfo entry
//                    };

//                    // Save UserLogin to the database
//                    _context.Userlogins.Add(userLogin);
//                    await _context.SaveChangesAsync();

//                    // Redirect to Login page after successful registration
//                    return RedirectToAction("Login", "Login");
//                }
//                catch (Exception ex)
//                {
//                    // Log the error (optional)
//                    Console.WriteLine($"Error: {ex.Message}");

//                    // Add a generic error message to the ModelState
//                    ModelState.AddModelError("", "An error occurred during registration. Please try again.");
//                }
//            }

//            // Reload the form with validation errors
//            return View(userinfo);
//        }

//    }
//}