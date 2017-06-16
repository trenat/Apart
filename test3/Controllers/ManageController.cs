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
            {
                return NotFound();
            }

            var eadiApartDbContext = _context.Apartment
                .Include(a => a.Owner)
                .Where(r => r.OwnerId == apUser.UserId)
                .Include(r => r.Owner.Reservation);
           
                
                
            return View(await eadiApartDbContext.ToListAsync());
        }

        // GET: Apartments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartment
                    .Include(a => a.Owner)
                    .SingleOrDefaultAsync(m => m.ApartmentId == id)
                ;
            if (apartment == null)
            {
                return NotFound();
            }



            return View(apartment);
        }

        // GET: Apartments/Create
        public IActionResult Create()
        {
            ViewData["OwnerId"] = new SelectList(_context.User, "UserId", "UserId");
            ViewData["Options"] = _context.Option.Select(x => x).ToList();
            return View();
        }

        // POST: Apartments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApartmentId,Adress,Description,Location,OwnerId,PriceBasic,RoomSize")] Apartment apartment)
        {
            if (ModelState.IsValid)
            {
               apartment.OwnerId = _userManager.GetUserAsync(HttpContext.User).Result.UserId;
                _context.Add(apartment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["OwnerId"] = new SelectList(_context.User, "UserId", "UserId", apartment.OwnerId);
            return View(apartment);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(ICollection<IFormFile> files)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(uploads, getReference()+id.ToString())+ ".jpg"), FileMode.Create))  // ID
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }
            return RedirectToAction("Index");
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