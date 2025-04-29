using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HMS.Models;

namespace HMS.Controllers
{
    public class MaintenanceController : Controller
    {
        private readonly ManagementSystemContext _context;

        public MaintenanceController(ManagementSystemContext context)
        {
            _context = context;
        }

        // GET: Maintenance
        public async Task<IActionResult> Index()
        {
            var managementSystemContext = _context.Maintenances.Include(m => m.Room);
            return View(await managementSystemContext.ToListAsync());
        }

        // GET: Maintenance/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenance = await _context.Maintenances
                .Include(m => m.Room)
                .FirstOrDefaultAsync(m => m.Maintenanceid == id);
            if (maintenance == null)
            {
                return NotFound();
            }

            return View(maintenance);
        }

        // GET: Maintenance/Create
        public IActionResult Create()
        {
            ViewData["Roomid"] = new SelectList(_context.Rooms, "Roomid", "Roomid");
            return View();
        }

        // POST: Maintenance/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Maintenanceid,Roomid,Issuedescription,Requestdate,Status,Maintenancetype")] Maintenance maintenance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maintenance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Roomid"] = new SelectList(_context.Rooms, "Roomid", "Roomid", maintenance.Roomid);
            return View(maintenance);
        }

        // GET: Maintenance/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenance = await _context.Maintenances.FindAsync(id);
            if (maintenance == null)
            {
                return NotFound();
            }
            ViewData["Roomid"] = new SelectList(_context.Rooms, "Roomid", "Roomid", maintenance.Roomid);
            return View(maintenance);
        }

        // POST: Maintenance/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Maintenanceid,Roomid,Issuedescription,Requestdate,Status,Maintenancetype")] Maintenance maintenance)
        {
            if (id != maintenance.Maintenanceid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(maintenance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaintenanceExists(maintenance.Maintenanceid))
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
            ViewData["Roomid"] = new SelectList(_context.Rooms, "Roomid", "Roomid", maintenance.Roomid);
            return View(maintenance);
        }

        // GET: Maintenance/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenance = await _context.Maintenances
                .Include(m => m.Room)
                .FirstOrDefaultAsync(m => m.Maintenanceid == id);
            if (maintenance == null)
            {
                return NotFound();
            }

            return View(maintenance);
        }

        // POST: Maintenance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var maintenance = await _context.Maintenances.FindAsync(id);
            if (maintenance != null)
            {
                _context.Maintenances.Remove(maintenance);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaintenanceExists(int id)
        {
            return _context.Maintenances.Any(e => e.Maintenanceid == id);
        }
    }
}
