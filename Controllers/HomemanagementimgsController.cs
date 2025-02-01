using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using yogago.Models;

namespace yogago.Controllers
{
    public class HomemanagementimgsController : Controller
    {
        private readonly ModelContext _context;

        public HomemanagementimgsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Homemanagementimgs
        public async Task<IActionResult> Index()
        {
              return _context.Homemanagementimgs != null ? 
                          View(await _context.Homemanagementimgs.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Homemanagementimgs'  is null.");
        }

        // GET: Homemanagementimgs/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Homemanagementimgs == null)
            {
                return NotFound();
            }

            var homemanagementimg = await _context.Homemanagementimgs
                .FirstOrDefaultAsync(m => m.Imgid == id);
            if (homemanagementimg == null)
            {
                return NotFound();
            }

            return View(homemanagementimg);
        }

        // GET: Homemanagementimgs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Homemanagementimgs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Homemanagementimg homemanagementimg)
        {
            //[Bind("Imgid,Imgname,Imgpath")]
            if (ModelState.IsValid)
            {

                homemanagementimg = new Homemanagementimg
                {
                    Imgname = homemanagementimg.Imgname,
                    Imgpath = homemanagementimg.Imgpath


                };
                     if(homemanagementimg.Imgfile!= null)
                {

                    string fileName = Path.GetFileName(homemanagementimg.Imgfile.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Homeimg/", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await homemanagementimg.Imgfile.CopyToAsync(stream);
                    }

                    // Save the file path in the database
                    homemanagementimg.Imgpath = "/Homeimg/" + fileName;
                }
           
          
            _context.Add(homemanagementimg);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(homemanagementimg);
        }

        // GET: Homemanagementimgs/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Homemanagementimgs == null)
            {
                return NotFound();
            }

            var homemanagementimg = await _context.Homemanagementimgs.FindAsync(id);
            if (homemanagementimg == null)
            {
                return NotFound();
            }
            return View(homemanagementimg);
        }

        // POST: Homemanagementimgs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Imgid,Imgname,Imgpath")] Homemanagementimg homemanagementimg)
        {
            if (id != homemanagementimg.Imgid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingUser = await _context.Homemanagementimgs.FindAsync(id);
                    if (existingUser == null)
                    {
                        return NotFound();
                    }
                    existingUser.Imgname = homemanagementimg.Imgname;


                    if (homemanagementimg.Imgfile != null)
                    {
                        string fileName = DateTime.Now.Ticks + "_" + Path.GetFileName(homemanagementimg.Imgfile.FileName);
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Homeimg", fileName);

                        // Ensure the directory exists
                        string directoryPath = Path.GetDirectoryName(filePath);
                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }

                        // Save the new file
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await homemanagementimg.Imgfile.CopyToAsync(stream);
                        }

                        // Update the profile picture path
                        existingUser.Imgpath = "/Homeimg/" + fileName;
                        //await _context.SaveChangesAsync();

                    }



                    _context.Update(homemanagementimg);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomemanagementimgExists(homemanagementimg.Imgid))
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
            return View(homemanagementimg);
        }

        // GET: Homemanagementimgs/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Homemanagementimgs == null)
            {
                return NotFound();
            }

            var homemanagementimg = await _context.Homemanagementimgs
                .FirstOrDefaultAsync(m => m.Imgid == id);
            if (homemanagementimg == null)
            {
                return NotFound();
            }

            return View(homemanagementimg);
        }

        // POST: Homemanagementimgs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Homemanagementimgs == null)
            {
                return Problem("Entity set 'ModelContext.Homemanagementimgs'  is null.");
            }
            var homemanagementimg = await _context.Homemanagementimgs.FindAsync(id);
            if (homemanagementimg != null)
            {
                _context.Homemanagementimgs.Remove(homemanagementimg);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomemanagementimgExists(decimal id)
        {
          return (_context.Homemanagementimgs?.Any(e => e.Imgid == id)).GetValueOrDefault();
        }
    }
}
