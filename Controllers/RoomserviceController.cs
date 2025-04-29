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
    public class RoomserviceController : Controller
    {
        private readonly ManagementSystemContext _context;

        public RoomserviceController(ManagementSystemContext context)
        {
            _context = context;
        }

        // GET: Roomservice
        public async Task<IActionResult> Index()
        {
            var managementSystemContext = _context.Roomservices.Include(r => r.Employee).Include(r => r.Room);
            return View(await managementSystemContext.ToListAsync());
        }

        // GET: Roomservice/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomservice = await _context.Roomservices
                .Include(r => r.Employee)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.Serviceid == id);
            if (roomservice == null)
            {
                return NotFound();
            }

            return View(roomservice);
        }

        // GET: Roomservice/Create
        public IActionResult Create()
        {
            ViewData["Employeeid"] = new SelectList(_context.Employees, "Employeeid", "Employeeid");
            ViewData["Roomid"] = new SelectList(_context.Rooms, "Roomid", "Roomid");
            return View();
        }

        // POST: Roomservice/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Serviceid,Roomid,Employeeid,Servicedate,Servicetype,Status,Servicedetails")] Roomservice roomservice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roomservice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Employeeid"] = new SelectList(_context.Employees, "Employeeid", "Employeeid", roomservice.Employeeid);
            ViewData["Roomid"] = new SelectList(_context.Rooms, "Roomid", "Roomid", roomservice.Roomid);
            return View(roomservice);
        }

        // GET: Roomservice/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomservice = await _context.Roomservices.FindAsync(id);
            if (roomservice == null)
            {
                return NotFound();
            }
            ViewData["Employeeid"] = new SelectList(_context.Employees, "Employeeid", "Employeeid", roomservice.Employeeid);
            ViewData["Roomid"] = new SelectList(_context.Rooms, "Roomid", "Roomid", roomservice.Roomid);
            return View(roomservice);
        }

        // POST: Roomservice/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Serviceid,Roomid,Employeeid,Servicedate,Servicetype,Status,Servicedetails")] Roomservice roomservice)
        {
            if (id != roomservice.Serviceid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roomservice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomserviceExists(roomservice.Serviceid))
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
            ViewData["Employeeid"] = new SelectList(_context.Employees, "Employeeid", "Employeeid", roomservice.Employeeid);
            ViewData["Roomid"] = new SelectList(_context.Rooms, "Roomid", "Roomid", roomservice.Roomid);
            return View(roomservice);
        }

        // GET: Roomservice/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomservice = await _context.Roomservices
                .Include(r => r.Employee)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.Serviceid == id);
            if (roomservice == null)
            {
                return NotFound();
            }

            return View(roomservice);
        }

        // POST: Roomservice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roomservice = await _context.Roomservices.FindAsync(id);
            if (roomservice != null)
            {
                _context.Roomservices.Remove(roomservice);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomserviceExists(int id)
        {
            return _context.Roomservices.Any(e => e.Serviceid == id);
        }
    }
}
