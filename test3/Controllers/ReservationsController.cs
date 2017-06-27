using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using test3.Data;
using test3.Models.ReservationViewModel;

namespace test3.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly eadiApartDbContext _context;

        public ReservationsController(eadiApartDbContext context)
        {
            _context = context;    
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var eadiApartDbContext = _context.Reservation
                .Include(r => r.Apartment)
                .Include(r => r.Client)
                .Include(r => r.Rate);
            return View(await eadiApartDbContext.ToListAsync());
        }

        [HttpPost]
        public  IActionResult View2(VIewViewModel model)
        {
            
            return View(model);
        }

        public  Task<IActionResult> View2()
        {
            VIewViewModel model = new VIewViewModel();
            model.Apartments = _context.Apartment;
            return View(model);
        }
        public IActionResult Search()
        {
            var Options = _context.Option.Select(x => x).ToList();
            var CheckableOptions = Options.Select(x => new IsOption(x)).ToList();
            return View(new SearchViewModel()
            {
                Apartments = _context.Apartment.ToList(),
                Options = CheckableOptions
            });
        }

        [HttpPost]
        public IActionResult Search(SearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                var Options = _context.Option.Select(x => x).ToList();
                var CheckableOptions = Options.Select(x => new IsOption(x));
                return View(new SearchViewModel()
                {
                    Options = CheckableOptions.ToList()
                });
            }
            else
            {

                return View(new SearchViewModel());
            }
        }


        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .Include(r => r.Apartment)
                .Include(r => r.Client)
                .Include(r => r.Rate)
                .SingleOrDefaultAsync(m => m.ReservationId == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            ViewData["ApartmentId"] = new SelectList(_context.Apartment, "ApartmentId", "Adress");
            ViewData["ClientId"] = new SelectList(_context.User, "UserId", "UserId");
            ViewData["RateId"] = new SelectList(_context.Rate, "RateId", "RateId");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservationId,ApartmentId,ClientId,Comment,FromDate,OwnerReply,RateId,Status,ToDate,TotalCost")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ApartmentId"] = new SelectList(_context.Apartment, "ApartmentId", "Adress", reservation.ApartmentId);
            ViewData["ClientId"] = new SelectList(_context.User, "UserId", "UserId", reservation.ClientId);
            ViewData["RateId"] = new SelectList(_context.Rate, "RateId", "RateId", reservation.RateId);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation.SingleOrDefaultAsync(m => m.ReservationId == id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["ApartmentId"] = new SelectList(_context.Apartment, "ApartmentId", "Adress", reservation.ApartmentId);
            ViewData["ClientId"] = new SelectList(_context.User, "UserId", "UserId", reservation.ClientId);
            ViewData["RateId"] = new SelectList(_context.Rate, "RateId", "RateId", reservation.RateId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReservationId,ApartmentId,ClientId,Comment,FromDate,OwnerReply,RateId,Status,ToDate,TotalCost")] Reservation reservation)
        {
            if (id != reservation.ReservationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.ReservationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["ApartmentId"] = new SelectList(_context.Apartment, "ApartmentId", "Adress", reservation.ApartmentId);
            ViewData["ClientId"] = new SelectList(_context.User, "UserId", "UserId", reservation.ClientId);
            ViewData["RateId"] = new SelectList(_context.Rate, "RateId", "RateId", reservation.RateId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .Include(r => r.Apartment)
                .Include(r => r.Client)
                .Include(r => r.Rate)
                .SingleOrDefaultAsync(m => m.ReservationId == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservation.SingleOrDefaultAsync(m => m.ReservationId == id);
            _context.Reservation.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservation.Any(e => e.ReservationId == id);
        }
    }
}
