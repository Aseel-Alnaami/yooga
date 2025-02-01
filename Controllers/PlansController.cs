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
    public class PlansController : Controller
    {
        private readonly ModelContext _context;

        public PlansController(ModelContext context)
        {
            _context = context;
        }

            //var username = HttpContext.Session.GetString("Username");
            //var Userid = HttpContext.Session.GetInt32("Userid");
            //var Rolename = HttpContext.Session.GetString("Rolename");
            //var Roleid = HttpContext.Session.GetInt32("Roleid");


            //if (string.IsNullOrEmpty(username))
            //{
            //    return RedirectToAction("Login", "Login");
            //}
            //ViewBag.Username = username;
        // GET: Plans
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
            if (_context.Plans == null)
            {
                return Problem("Entity set 'ModelContext.Plans' is null.");
            }
            var planinfo = await _context.Plans.ToListAsync();





            return View(planinfo); // withot view the palninfo is null

        }

        


            // GET: Plans/Details/5
            public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Plans == null)
            {
                return NotFound();
            }

            var plan = await _context.Plans
                .FirstOrDefaultAsync(m => m.Planid == id);
            if (plan == null)
            {
                return NotFound();
            }

            return View(plan);
        }

        // GET: Plans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Plans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Planid,Planname,Durationmonths,Price")] Plan plan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }

        // GET: Plans/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Plans == null)
            {
                return NotFound();
            }

            var plan = await _context.Plans.FindAsync(id);
            if (plan == null)
            {
                return NotFound();
            }
            return View(plan);
        }

        // POST: Plans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Planid,Planname,Durationmonths,Price")] Plan plan)
        {
            if (id != plan.Planid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanExists(plan.Planid))
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
            return View(plan);
        }

        // GET: Plans/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Plans == null)
            {
                return NotFound();
            }

            var plan = await _context.Plans
                .FirstOrDefaultAsync(m => m.Planid == id);
            if (plan == null)
            {
                return NotFound();
            }

            return View(plan);
        }

        // POST: Plans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Plans == null)
            {
                return Problem("Entity set 'ModelContext.Plans'  is null.");
            }
            var plan = await _context.Plans.FindAsync(id);
            if (plan != null)
            {
                _context.Plans.Remove(plan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanExists(decimal id)
        {
          return (_context.Plans?.Any(e => e.Planid == id)).GetValueOrDefault();
        }
    }
}
