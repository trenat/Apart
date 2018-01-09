using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using test3.Data;
using test3.Services;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using test3.Models.ManageViewModels;
namespace test3.Controllers
{
    public class ManageController : Controller
    {
        private readonly eadiApartDbContext _context;
        private readonly IHostingEnvironment _environment;
        private MyUserManager _userManager;

        public ManageController(eadiApartDbContext context, IHostingEnvironment environment, MyUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
            _environment = environment;
        }

        // GET: Apartments
        public async Task<IActionResult> Index()
        {
            var apUser = await _userManager.GetUserAsync(HttpContext.User);

            if (apUser == null)
                return NotFound();

            var eadiApartDbContext = _context.Apartment
                .Include(a => a.Owner)
                .Where(r => r.OwnerId == apUser.UserId)
                .Include(r => r.Owner.Reservation)
                .Include(o => o.ApartOption)
                .ThenInclude(o=> o.Option);
            

            return View(new ManageIndexViewModel()
            {
                Apartments = eadiApartDbContext,
                User = apUser,
                Reservations = _context.Reservation
                                       .Include(r => r.Apartment)
                                       .Where(x => x.ClientId == apUser.UserId).ToList()
                
            });
        }

        // GET: Apartments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var apartment = await _context.Apartment
                .Include(a => a.Owner)
                .SingleOrDefaultAsync(m => m.ApartmentId == id);

            if (apartment == null)
                return NotFound();

            return View(apartment);
        }

        // GET: Apartments/Create
        public IActionResult Create()
        {
            ViewData["OwnerId"] = new SelectList(_context.User, "UserId", "UserId");
            //ViewData["Options"] = _context.Option.Select(x => x).ToList();
            var model = new test3.Models.ManageViewModels.CreateApartmentViewModel()
            {
                Options = _context.Option.Select(x => new Opt() { Name = x.Name, isChecked = false }).ToList(),
                Apartment = new Apartment()
            };
            return View(model);
        }

        // POST: Apartments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(test3.Models.ManageViewModels.CreateApartmentViewModel model, ICollection<IFormFile> files)
        {
            Apartment apartment = model.Apartment;
            if (ModelState.IsValid)
            {
                apartment.OwnerId = _userManager.GetUserAsync(HttpContext.User).Result.UserId;
                apartment.ApartOption = new List<ApartOption>();

                _context.Option.Load();
                foreach (var option in model.Options)
                {
                    if (option.isChecked == true)
                    {
                        Option opt = _context.Option.Local.FirstOrDefault(x => x.Name == option.Name);
                        apartment.ApartOption.Add(new ApartOption()
                        {
                            Apartment = apartment,
                            OptionId =  opt.OptionId
                        });
                    }
                }

                _context.Add(apartment);
                _context.SaveChanges();
                var uploads = Path.Combine(_environment.WebRootPath, "uploads");
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        using (var fileStream = new FileStream(Path.Combine(uploads, Guid.NewGuid().GetHashCode().ToString() + ".jpg"), FileMode.Create))  // ID
                        {
                            await file.CopyToAsync(fileStream);
                        }
                    }
                }

                ViewData["OwnerId"] = new SelectList(_context.User, "UserId", "UserId", apartment.OwnerId);
                return RedirectToAction("Index");
            }

            return View(model);
        }



        // GET: Apartments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartment.SingleOrDefaultAsync(m => m.ApartmentId == id);
            if (apartment == null)
            {
                return NotFound();
            }
            ViewData["OwnerId"] = new SelectList(_context.User, "UserId", "UserId", apartment.OwnerId);
            return View(apartment);
        }

        // POST: Apartments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApartmentId,Adress,Description,Location,OwnerId,PriceBasic,RoomSize")] Apartment apartment)
        {
            if (id != apartment.ApartmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(apartment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApartmentExists(apartment.ApartmentId))
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
            ViewData["OwnerId"] = new SelectList(_context.User, "UserId", "UserId", apartment.OwnerId);
            return View(apartment);
        }

        // GET: Apartments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartment
                .Include(a => a.Owner)
                .SingleOrDefaultAsync(m => m.ApartmentId == id);
            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }

        // POST: Apartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var apartment = await _context.Apartment.SingleOrDefaultAsync(m => m.ApartmentId == id);
            _context.Apartment.Remove(apartment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ApartmentExists(int id)
        {
            return _context.Apartment.Any(e => e.ApartmentId == id);
        }
    }
}