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
    public class ReviewController : Controller
    {
        private readonly ManagementSystemContext _context;

        public ReviewController(ManagementSystemContext context)
        {
            _context = context;
        }

        // GET: Review
        public async Task<IActionResult> Index()
        {
            var managementSystemContext = _context.Reviews.Include(r => r.Guest).Include(r => r.Room).Include(r => r.Service);
            return View(await managementSystemContext.ToListAsync());
        }

        // GET: Review/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews
                .Include(r => r.Guest)
                .Include(r => r.Room)
                .Include(r => r.Service)
                .FirstOrDefaultAsync(m => m.Reviewid == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Review/Create
        public IActionResult Create()
        {
            ViewData["Guestid"] = new SelectList(_context.Guests, "Guestid", "Guestid");
            ViewData["Roomid"] = new SelectList(_context.Rooms, "Roomid", "Roomid");
            ViewData["Serviceid"] = new SelectList(_context.Roomservices, "Serviceid", "Serviceid");
            return View();
        }

        // POST: Review/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Reviewid,Guestid,Roomid,Serviceid,Rating,Reviewtext,Reviewdate,Title")] Review review)
        {
            if (ModelState.IsValid)
            {
                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Guestid"] = new SelectList(_context.Guests, "Guestid", "Guestid", review.Guestid);
            ViewData["Roomid"] = new SelectList(_context.Rooms, "Roomid", "Roomid", review.Roomid);
            ViewData["Serviceid"] = new SelectList(_context.Roomservices, "Serviceid", "Serviceid", review.Serviceid);
            return View(review);
        }

        // GET: Review/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            ViewData["Guestid"] = new SelectList(_context.Guests, "Guestid", "Guestid", review.Guestid);
            ViewData["Roomid"] = new SelectList(_context.Rooms, "Roomid", "Roomid", review.Roomid);
            ViewData["Serviceid"] = new SelectList(_context.Roomservices, "Serviceid", "Serviceid", review.Serviceid);
            return View(review);
        }

        // POST: Review/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Reviewid,Guestid,Roomid,Serviceid,Rating,Reviewtext,Reviewdate,Title")] Review review)
        {
            if (id != review.Reviewid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(review);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.Reviewid))
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
            ViewData["Guestid"] = new SelectList(_context.Guests, "Guestid", "Guestid", review.Guestid);
            ViewData["Roomid"] = new SelectList(_context.Rooms, "Roomid", "Roomid", review.Roomid);
            ViewData["Serviceid"] = new SelectList(_context.Roomservices, "Serviceid", "Serviceid", review.Serviceid);
            return View(review);
        }

        // GET: Review/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews
                .Include(r => r.Guest)
                .Include(r => r.Room)
                .Include(r => r.Service)
                .FirstOrDefaultAsync(m => m.Reviewid == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Review/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.Reviewid == id);
        }
    }
}
