using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration;
using test3.Data;
using test3.Models.ReservationViewModel;

namespace test3.Controllers
{
    public static class InRangeExtensions
    {
        public static bool InRange(this DateTime? dateToCheck, DateTime? startDate, DateTime? endDate)
        {
            return dateToCheck >= startDate && dateToCheck <= endDate;
        }
        public static bool InRange(this int? valueToCheck, int? minValue, int? maxValue)
        {
            return valueToCheck >= minValue && valueToCheck <= maxValue;
        }
        public static bool InRange(this decimal valueToCheck, decimal minValue, decimal maxValue)
        {
            return valueToCheck >= minValue && valueToCheck <= maxValue;
        }
        public static bool InRange(this decimal? valueToCheck, decimal? minValue, decimal? maxValue)
        {
            return valueToCheck >= minValue && valueToCheck <= maxValue;
        }
    }

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

        //[HttpPost]
        //public IActionResult View2(SearchViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var Options = _context.Option.Select(x => x).ToList();
        //        var CheckableOptions = Options.Select(x => new IsOption()
        //        {
        //            Name = x.Name,
        //            IsSet = false
        //        });
        //        return View(new SearchViewModel()
        //        {
        //            Options = CheckableOptions.ToList()
        //        });
        //    }
        //    else
        //    {

        //        return View(new SearchViewModel());
        //    }
        //}

        //public IActionResult View2()
        //{
        //    var Options = _context.Option.Select(x => x).ToList();
        //    var CheckableOptions = Options.Select(x => new IsOption()
        //    {
        //        Name = x.Name,
        //        IsSet = false
        //    }).ToList();
        //    var model = new SearchViewModel()
        //    {
        //        Apartments = _context.Apartment.ToList(),
        //        Options = CheckableOptions
        //    };
        //    return View(model);
        //}


        public IActionResult Search()
        {

            var Options = _context.Option.Select(x => x).ToList();
            var CheckableOptions = Options.Select(x => new IsOption()
            {
                Name = x.Name,
                IsSet = false
            }).ToList();
            var model = new SearchViewModel()
            {
                Apartments = _context.Apartment.ToList(),
                Options = CheckableOptions
            };
            return View(model);
        }



        [HttpPost]
        public IActionResult Search(SearchViewModel model)
        {

            Func<Apartment,bool> DateFilter = (Apartment ap) => true;
            Func<Apartment,bool> PriceFilter = (Apartment ap) => true;
            Func<Apartment, bool> RateFilter = (Apartment ap) => true;
            Func<Apartment, bool> OptionsFilter = (Apartment ap) =>
            {
                if (model.Options.Where(x => x.IsSet)                
                                 .All(x => ap.ApartOption.Any(op => op.Option.Name == x.Name)))  
                    return true;
                return false;
            };
            if (model.UseDate)
            {
                if (model.DateFrom.CompareTo(model.DateTo) > 0)
                {
                    if (model.Apartments != null)
                        model.Apartments.Clear();
                    return View(model);

                }
                DateFilter = (Apartment ap) =>
                {
                    if (ap.Reservation.Any(res => res.FromDate.InRange(model.DateFrom, model.DateTo) &&
                                                  res.ToDate.InRange(model.DateFrom, model.DateTo)))     //If there is any conflict in dates
                        return false;
                    return true;
                };
            }
            if (model.UsePrice) 
            {
                if (model.PriceMin < model.PriceMax)
                {
                    if (model.Apartments != null)
                        model.Apartments.Clear();
                    return View(model);

                }
                PriceFilter = (Apartment ap) =>
                {
                    if (ap.PriceBasic.InRange(model.PriceMin, model.PriceMax))
                        return true;
                    return false;
                };
            }
            if (model.UseRate) 
            {
                if (model.RateMin < model.RateMax)
                {
                    if (model.Apartments != null)
                        model.Apartments.Clear();
                    return View(model);

                }
                RateFilter = (Apartment ap) =>
                {
                    decimal? avg = ap.Reservation.Average(x => x.Rate?.RateLevel);
                    if (avg!=null && avg.InRange(model.RateMin, model.RateMax))
                        return true;
                    return false;
                };
            }
            if(model.Apartments!=null)
                model.Apartments.Clear();

            model.Apartments = _context.Apartment
                .Include(ap => ap.Reservation)
                .ThenInclude(ap => ap.Rate)
                .Where(PriceFilter)
                .Where(RateFilter)
                .Where(DateFilter)
                .Where(OptionsFilter)
                .ToList();

            return View(model);
            if (ModelState.IsValid)
            {
                return View(model);
            }
            else
            {

                return View(model);
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
