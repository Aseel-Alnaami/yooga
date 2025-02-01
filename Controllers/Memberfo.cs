using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using yogago.Models;

namespace yogago.Controllers
{
    public class Memberfo : Controller
    {
        private readonly ModelContext _context;

        public Memberfo(ModelContext context)
        {
            _context = context;
        }

        // GET: Memberfo
        public async Task<IActionResult> Index()
        {
            var username = HttpContext.Session.GetString("Username");
            var Userid = HttpContext.Session.GetInt32("Userid");
            var Rolename = HttpContext.Session.GetString("Rolename");
            var Roleid = HttpContext.Session.GetInt32("Roleid");


            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Login");
            }
            ViewBag.Username = username;
            var profileimg = HttpContext.Session.GetString("Profileimg");

            ViewBag.img = profileimg;

            var modelContext = _context.Userinfos.Include(u => u.Role).Where(u => u.Roleid == 3); 
            return View(await modelContext.ToListAsync());
        }

        // GET: Memberfo/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Userinfos == null)
            {
                return NotFound();
            }

            var userinfo = await _context.Userinfos
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (userinfo == null)
            {
                return NotFound();
            }

            return View(userinfo);
        }

        // GET: Memberfo/Create
        public IActionResult Create()
        {
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid");
            return View();
        }

        // POST: Memberfo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Class2 class2)
        {
            if (ModelState.IsValid)
            {
                // Map Class2 to Userinfo
                Userinfo userinfo = new Userinfo
                {
                    Fullname = class2.Fullname,
                    Username = class2.Username,
                    Email = class2.Email,
                    Phone = class2.Phone,
                    Dateofbirth = class2.Dateofbirth,
                    Address = class2.Address,
                    Profilepicture = class2.Profilepicture, // Assuming this holds the path or URL
                    Password = class2.Password,
                    Roleid = 3, // Roleid to 3 for member
                    Dateadded = DateTime.Now
                };

                // Handle file upload if Profilepicturefile is provided
                if (class2.Profilepicturefile != null)
                {
                    string fileName = Path.GetFileName(class2.Profilepicturefile.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image/", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await class2.Profilepicturefile.CopyToAsync(stream);
                    }

                    // Save the file path in the database
                    userinfo.Profilepicture = "/image/" + fileName;
                }

                _context.Add(userinfo);
                await _context.SaveChangesAsync();


                var member = new Member
                {
                    Userid = userinfo.Userid,
                    Joindate = DateTime.Now
                };

                _context.Members.Add(member);
                _context.SaveChanges();



                return RedirectToAction(nameof(Index));
            }

            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", 3); // Default value in case of error
            return View(class2);
        }
        


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Userid,Fullname,Username,Email,Phone,Dateofbirth,Address,Profilepicture,Password,Roleid,Dateadded")] Userinfo userinfo)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(userinfo);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", userinfo.Roleid);
        //    return View(userinfo);
        //}

        // GET: Memberfo/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Userinfos == null)
            {
                return NotFound();
            }

            //var userinfo = await _context.Userinfos.FindAsync(id);
            var userinfo = await _context.Userinfos.Include(u => u.Role).FirstOrDefaultAsync(u => u.Userid == id);

            if (userinfo == null)
            {
                return NotFound();
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", userinfo.Roleid);
            return View(userinfo);
        }

        // POST: Memberfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, Class2 class2)
        {
            if (id != class2.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Retrieve the existing user from the database
                    var existingUser = await _context.Userinfos.FindAsync(id);
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

                        // Ensure the directory exists
                        string directoryPath = Path.GetDirectoryName(filePath);
                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }

                        // Save the new file
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await class2.Profilepicturefile.CopyToAsync(stream);
                        }

                        // Update the profile picture path
                        existingUser.Profilepicture = "/image/" + fileName;
                    }

                    // Save changes to the database
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserinfoExists(class2.Userid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            // Repopulate dropdown for validation errors
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "RoleName", class2.Roleid);
            return View(class2);
        }



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(decimal id, [Bind("Userid,Fullname,Username,Email,Phone,Dateofbirth,Address,Profilepicture,Password,Roleid,Dateadded")] Userinfo userinfo)
        //{
        //    if (id != userinfo.Userid)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(userinfo);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!UserinfoExists(userinfo.Userid))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", userinfo.Roleid);
        //    return View(userinfo);
        //}

        // GET: Memberfo/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Userinfos == null)
            {
                return NotFound();
            }

            var userinfo = await _context.Userinfos
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (userinfo == null)
            {
                return NotFound();
            }

            return View(userinfo);
        }

        // POST: Memberfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Userinfos == null)
            {
                return Problem("Entity set 'ModelContext.Userinfos'  is null.");
            }
            var userinfo = await _context.Userinfos.FindAsync(id);
            if (userinfo != null)
            {
                _context.Userinfos.Remove(userinfo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserinfoExists(decimal id)
        {
          return (_context.Userinfos?.Any(e => e.Userid == id)).GetValueOrDefault();
        }
    }
}
