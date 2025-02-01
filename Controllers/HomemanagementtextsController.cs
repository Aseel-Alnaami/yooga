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
    public class HomemanagementtextsController : Controller
    {
        private readonly ModelContext _context;

        public HomemanagementtextsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Homemanagementtexts
        public async Task<IActionResult> Index()
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
            return _context.Homemanagementtexts != null ? 
                          View(await _context.Homemanagementtexts.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Homemanagementtexts'  is null.");
        }

        // GET: Homemanagementtexts/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Homemanagementtexts == null)
            {
                return NotFound();
            }

            var homemanagementtext = await _context.Homemanagementtexts
                .FirstOrDefaultAsync(m => m.Textid == id);
            if (homemanagementtext == null)
            {
                return NotFound();
            }

            return View(homemanagementtext);
        }

        // GET: Homemanagementtexts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Homemanagementtexts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Textid,Textname,Content")] Homemanagementtext homemanagementtext)
        {
            if (ModelState.IsValid)
            {
                _context.Add(homemanagementtext);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(homemanagementtext);
        }

        // GET: Homemanagementtexts/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Homemanagementtexts == null)
            {
                return NotFound();
            }

            var homemanagementtext = await _context.Homemanagementtexts.FindAsync(id);
            if (homemanagementtext == null)
            {
                return NotFound();
            }
            return View(homemanagementtext);
        }

        // POST: Homemanagementtexts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Textid,Textname,Content")] Homemanagementtext homemanagementtext)
        {
            if (id != homemanagementtext.Textid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(homemanagementtext);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomemanagementtextExists(homemanagementtext.Textid))
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
            return View(homemanagementtext);
        }

        // GET: Homemanagementtexts/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Homemanagementtexts == null)
            {
                return NotFound();
            }

            var homemanagementtext = await _context.Homemanagementtexts
                .FirstOrDefaultAsync(m => m.Textid == id);
            if (homemanagementtext == null)
            {
                return NotFound();
            }

            return View(homemanagementtext);
        }

        // POST: Homemanagementtexts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Homemanagementtexts == null)
            {
                return Problem("Entity set 'ModelContext.Homemanagementtexts'  is null.");
            }
            var homemanagementtext = await _context.Homemanagementtexts.FindAsync(id);
            if (homemanagementtext != null)
            {
                _context.Homemanagementtexts.Remove(homemanagementtext);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomemanagementtextExists(decimal id)
        {
          return (_context.Homemanagementtexts?.Any(e => e.Textid == id)).GetValueOrDefault();
        }
    }
}
