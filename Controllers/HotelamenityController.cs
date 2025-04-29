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
    public class HotelamenityController : Controller
    {
        private readonly ManagementSystemContext _context;

        public HotelamenityController(ManagementSystemContext context)
        {
            _context = context;
        }

        // GET: Hotelamenity
        public async Task<IActionResult> Index()
        {
            return View(await _context.Hotelamenities.ToListAsync());
        }

        // GET: Hotelamenity/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotelamenity = await _context.Hotelamenities
                .FirstOrDefaultAsync(m => m.Amenityid == id);
            if (hotelamenity == null)
            {
                return NotFound();
            }

            return View(hotelamenity);
        }

        // GET: Hotelamenity/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hotelamenity/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Amenityid,Amenityname,Description,Availabilitystatus")] Hotelamenity hotelamenity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hotelamenity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hotelamenity);
        }

        // GET: Hotelamenity/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotelamenity = await _context.Hotelamenities.FindAsync(id);
            if (hotelamenity == null)
            {
                return NotFound();
            }
            return View(hotelamenity);
        }

        // POST: Hotelamenity/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Amenityid,Amenityname,Description,Availabilitystatus")] Hotelamenity hotelamenity)
        {
            if (id != hotelamenity.Amenityid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotelamenity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelamenityExists(hotelamenity.Amenityid))
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
            return View(hotelamenity);
        }

        // GET: Hotelamenity/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotelamenity = await _context.Hotelamenities
                .FirstOrDefaultAsync(m => m.Amenityid == id);
            if (hotelamenity == null)
            {
                return NotFound();
            }

            return View(hotelamenity);
        }

        // POST: Hotelamenity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hotelamenity = await _context.Hotelamenities.FindAsync(id);
            if (hotelamenity != null)
            {
                _context.Hotelamenities.Remove(hotelamenity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HotelamenityExists(int id)
        {
            return _context.Hotelamenities.Any(e => e.Amenityid == id);
        }
    }
}
