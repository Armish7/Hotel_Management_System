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
    public class PaymentController : Controller
    {
        private readonly ManagementSystemContext _context;

        public PaymentController(ManagementSystemContext context)
        {
            _context = context;
        }

        // GET: Payment
        public async Task<IActionResult> Index()
        {
            var managementSystemContext = _context.Payments.Include(p => p.Booking);
            return View(await managementSystemContext.ToListAsync());
        }

        // GET: Payment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.Booking)
                .FirstOrDefaultAsync(m => m.Paymentid == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payment/Create
        public IActionResult Create()
        {
            ViewData["Bookingid"] = new SelectList(_context.Bookings, "Bookingid", "Bookingid");
            return View();
        }

        // POST: Payment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Paymentid,Bookingid,Paymentdate,Amount,Paymentmethod,Paymentstatus,Transactionnumber")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Bookingid"] = new SelectList(_context.Bookings, "Bookingid", "Bookingid", payment.Bookingid);
            return View(payment);
        }

        // GET: Payment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            ViewData["Bookingid"] = new SelectList(_context.Bookings, "Bookingid", "Bookingid", payment.Bookingid);
            return View(payment);
        }

        // POST: Payment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Paymentid,Bookingid,Paymentdate,Amount,Paymentmethod,Paymentstatus,Transactionnumber")] Payment payment)
        {
            if (id != payment.Paymentid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.Paymentid))
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
            ViewData["Bookingid"] = new SelectList(_context.Bookings, "Bookingid", "Bookingid", payment.Bookingid);
            return View(payment);
        }

        // GET: Payment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.Booking)
                .FirstOrDefaultAsync(m => m.Paymentid == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.Paymentid == id);
        }
    }
}
