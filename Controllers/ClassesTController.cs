using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using yogago.Models;

namespace yogago.Controllers
{
    public class ClassesTController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ClassesTController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }


        // GET: ClassesT
        //public async Task<IActionResult> Index()
        //{
        //    var modelContext = _context.Classes.Include(x => x.Category).Include(x => x.Trainer);
        //    return View(await modelContext.ToListAsync());
        //}


        public async Task<IActionResult> Index()
        {
            // Retrieve Trainerid (Userid) from session
            var trainerid = HttpContext.Session.GetInt32("Userid");

            if (trainerid == null)
            {
                // If the session is not set, redirect to the login page
                return RedirectToAction("Login", "Login");
            }

            // Filter data for the logged-in trainer
            var modelContext = _context.Classes
                .Include(x => x.Category)
                .Include(x => x.Trainer)
                .Where(x => x.Trainerid == trainerid); // Match Trainerid with session Userid

            return View(await modelContext.ToListAsync());
        }
        // GET: ClassesT/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Classes == null)
            {
                return NotFound();
            }

            var @class = await _context.Classes
                .Include(x => x.Category)
                .Include(x => x.Trainer)
                .FirstOrDefaultAsync(m => m.Classid == id);
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // GET: ClassesT/Create
        public IActionResult Create()
        {
            var trainerid = HttpContext.Session.GetInt32("Userid");
            if (trainerid == null)
            {
                return RedirectToAction("Login", "Login"); // Redirect to login if session is not set
            }

            ViewBag.Trainerid = trainerid;

            ViewData["Categoryid"] = new SelectList(
                    _context.Classcategories.OrderBy(c => c.Categoryname),
                    "Categoryid",
                    "Categoryname"
                );
            //ViewData["Trainerid"] = new SelectList(_context.Trainers, "Trainerid", "Trainerid");
            return View();
        }

        // POST: ClassesT/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Classid,Classname,Classdays,Classtime,Imgcover,Categoryid")] Class @class, IFormFile imgFile)
        {
            // Retrieve Trainerid from session
            var trainerid = HttpContext.Session.GetInt32("Userid");
            if (trainerid == null)
            {
                TempData["ErrorMessage"] = "Your session has expired. Please log in again.";
                return RedirectToAction("Login", "Login");
            }

            // Assign Trainerid from session
            @class.Trainerid = trainerid;

            if (ModelState.IsValid)
            {
                // Simplified image upload
                if (imgFile != null && imgFile.Length > 0)
                {
                    string wwwrootPath = _webHostEnvironment.WebRootPath; // Get the path to wwwroot
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imgFile.FileName); // Unique file name
                    string uploadPath = Path.Combine(wwwrootPath, "classcover"); // Target folder for uploads

                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath); // Create folder if it doesn't exist
                    }

                    string filePath = Path.Combine(uploadPath, fileName);

                    // Save the file to the server
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imgFile.CopyToAsync(fileStream);
                    }

                    // Save the relative path to the database
                    @class.Imgcover = Path.Combine("classcover", fileName).Replace("\\", "/");
                }

                // Add the class to the database
                _context.Add(@class);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Repopulate the Category dropdown if ModelState is invalid
            ViewData["Categoryid"] = new SelectList(
                _context.Classcategories.OrderBy(c => c.Categoryname),
                "Categoryid",
                "Categoryname",
                @class.Categoryid
            );

            return View(@class);
        }




        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Classid,Classname,Classdays,Classtime,Imgcover,Categoryid")] Class @class, IFormFile imgFile)
        //{
        //    // Retrieve Trainerid from session
        //    var trainerid = HttpContext.Session.GetInt32("Userid");
        //    if (trainerid == null)
        //    {
        //        return RedirectToAction("Login", "Login"); // Redirect to login if session is not set
        //    }

        //    // Assign Trainerid from session
        //    @class.Trainerid = trainerid;

        //    if (ModelState.IsValid)
        //    {




        //            //creation fun 
        //            _context.Add(@class);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    // Repopulate the Category dropdown if ModelState is invalid
        //    ViewData["Categoryid"] = new SelectList(
        //        _context.Classcategories.OrderBy(c => c.Categoryname),
        //        "Categoryid",
        //        "Categoryname",
        //        @class.Categoryid
        //    );

        //    return View(@class);
        //}



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Classid,Classname,Classdays,Classtime,Imgcover,Categoryid,Trainerid")] Class @class)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(@class);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["Categoryid"] = new SelectList(_context.Classcategories, "Categoryid", "Categoryid", @class.Categoryid);
        //    ViewData["Trainerid"] = new SelectList(_context.Trainers, "Trainerid", "Trainerid", @class.Trainerid);
        //    return View(@class);
        //}

        // GET: ClassesT/Edit/5

        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Classes == null)
            {
                return NotFound();
            }

            var @class = await _context.Classes.FindAsync(id);
            if (@class == null)
            {
                return NotFound();
            }

            // Populate ViewData["Categoryid"] with categories
            ViewData["Categoryid"] = new SelectList(
                _context.Classcategories.OrderBy(c => c.Categoryname),
                "Categoryid",
                "Categoryname",
                @class.Categoryid
            );

            return View(@class);
        }



        
        // POST: ClassesT/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Classid,Classname,Classdays,Classtime,Imgcover,Categoryid")] Class @class)
        {
            if (id != @class.Classid)
            {
                return NotFound();
            }

            // Retrieve Trainerid from session
            var trainerid = HttpContext.Session.GetInt32("Userid");
            if (trainerid == null)
            {
                return RedirectToAction("Login", "Login"); // Redirect if session is not set
            }

            @class.Trainerid = trainerid; // Assign Trainerid from session

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@class);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassExists(@class.Classid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewData["Categoryid"] = new SelectList(_context.Classcategories, "Categoryid", "Categoryid", @class.Categoryid);
            return View(@class);
        }






        // GET: ClassesT/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Classes == null)
            {
                return NotFound();
            }

            var @class = await _context.Classes
                .Include(x => x.Category)
                .Include(x => x.Trainer)
                .FirstOrDefaultAsync(m => m.Classid == id);
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // POST: ClassesT/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Classes == null)
            {
                return Problem("Entity set 'ModelContext.Classes'  is null.");
            }
            var @class = await _context.Classes.FindAsync(id);
            if (@class != null)
            {
                _context.Classes.Remove(@class);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassExists(decimal id)
        {
          return (_context.Classes?.Any(e => e.Classid == id)).GetValueOrDefault();
        }
    }
}
