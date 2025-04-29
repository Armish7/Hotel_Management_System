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
    public class RoomamenityController : Controller
    {
        private readonly ManagementSystemContext _context;

        public RoomamenityController(ManagementSystemContext context)
        {
            _context = context;
        }

        // GET: Roomamenity
        public async Task<IActionResult> Index()
        {
            var managementSystemContext = _context.Roomamenities.Include(r => r.Amenity).Include(r => r.Room);
            return View(await managementSystemContext.ToListAsync());
        }

        // GET: Roomamenity/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomamenity = await _context.Roomamenities
                .Include(r => r.Amenity)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.Roomamenityid == id);
            if (roomamenity == null)
            {
                return NotFound();
            }

            return View(roomamenity);
        }

        // GET: Roomamenity/Create
        public IActionResult Create()
        {
            ViewData["Amenityid"] = new SelectList(_context.Hotelamenities, "Amenityid", "Amenityid");
            ViewData["Roomid"] = new SelectList(_context.Rooms, "Roomid", "Roomid");
            return View();
        }

        // POST: Roomamenity/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Roomamenityid,Roomid,Amenityid,Assigneddate")] Roomamenity roomamenity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roomamenity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Amenityid"] = new SelectList(_context.Hotelamenities, "Amenityid", "Amenityid", roomamenity.Amenityid);
            ViewData["Roomid"] = new SelectList(_context.Rooms, "Roomid", "Roomid", roomamenity.Roomid);
            return View(roomamenity);
        }

        // GET: Roomamenity/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomamenity = await _context.Roomamenities.FindAsync(id);
            if (roomamenity == null)
            {
                return NotFound();
            }
            ViewData["Amenityid"] = new SelectList(_context.Hotelamenities, "Amenityid", "Amenityid", roomamenity.Amenityid);
            ViewData["Roomid"] = new SelectList(_context.Rooms, "Roomid", "Roomid", roomamenity.Roomid);
            return View(roomamenity);
        }

        // POST: Roomamenity/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Roomamenityid,Roomid,Amenityid,Assigneddate")] Roomamenity roomamenity)
        {
            if (id != roomamenity.Roomamenityid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roomamenity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomamenityExists(roomamenity.Roomamenityid))
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
            ViewData["Amenityid"] = new SelectList(_context.Hotelamenities, "Amenityid", "Amenityid", roomamenity.Amenityid);
            ViewData["Roomid"] = new SelectList(_context.Rooms, "Roomid", "Roomid", roomamenity.Roomid);
            return View(roomamenity);
        }

        // GET: Roomamenity/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomamenity = await _context.Roomamenities
                .Include(r => r.Amenity)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.Roomamenityid == id);
            if (roomamenity == null)
            {
                return NotFound();
            }

            return View(roomamenity);
        }

        // POST: Roomamenity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roomamenity = await _context.Roomamenities.FindAsync(id);
            if (roomamenity != null)
            {
                _context.Roomamenities.Remove(roomamenity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomamenityExists(int id)
        {
            return _context.Roomamenities.Any(e => e.Roomamenityid == id);
        }
    }
}
