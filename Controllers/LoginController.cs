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
                HttpContext.Session.SetString("Profileimg", user.Profilepicture);

                return user.Roleid switch
                {
                    1 => RedirectToAction("Subsecrebtions", "Admin"),
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
        public async Task<IActionResult>  Signup(Class2 class2)
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
                    // Ensure the directory exists
                    string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image/");
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    // Generate file name and full path
                    string fileName = Path.GetFileName(class2.Profilepicturefile.FileName);
                    string filePath = Path.Combine(directoryPath, fileName);

                    // Save the file in the directory
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await class2.Profilepicturefile.CopyToAsync(stream);
                    }

                    // Save the path including 'wwwroot' to the database
                    userInfo.Profilepicture = Path.Combine("/image/", fileName);
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