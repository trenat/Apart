using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using test3.Data;

namespace test3.Controllers
{
    public class ApartImagesController : Controller
    {
        private readonly eadiApartDbContext _context;

        public ApartImagesController(eadiApartDbContext context)
        {
            _context = context;    
        }

        // GET: ApartImages
        public async Task<IActionResult> Index()
        {
            var eadiApartDbContext = _context.ApartImage.Include(a => a.Apartment);
            return View(await eadiApartDbContext.ToListAsync());
        }

        // GET: ApartImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartImage = await _context.ApartImage
                .Include(a => a.Apartment)
                .SingleOrDefaultAsync(m => m.ImageId == id);
            if (apartImage == null)
            {
                return NotFound();
            }

            return View(apartImage);
        }

        // GET: ApartImages/Create
        public IActionResult Create()
        {
            ViewData["ApartmentId"] = new SelectList(_context.Apartment, "ApartmentId", "Adress");
            return View();
        }

        // POST: ApartImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImageId,ApartmentId,ImagePath")] ApartImage apartImage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(apartImage);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ApartmentId"] = new SelectList(_context.Apartment, "ApartmentId", "Adress", apartImage.ApartmentId);
            return View(apartImage);
        }

        // GET: ApartImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartImage = await _context.ApartImage.SingleOrDefaultAsync(m => m.ImageId == id);
            if (apartImage == null)
            {
                return NotFound();
            }
            ViewData["ApartmentId"] = new SelectList(_context.Apartment, "ApartmentId", "Adress", apartImage.ApartmentId);
            return View(apartImage);
        }

        // POST: ApartImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ImageId,ApartmentId,ImagePath")] ApartImage apartImage)
        {
            if (id != apartImage.ImageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(apartImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApartImageExists(apartImage.ImageId))
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
            ViewData["ApartmentId"] = new SelectList(_context.Apartment, "ApartmentId", "Adress", apartImage.ApartmentId);
            return View(apartImage);
        }

        // GET: ApartImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartImage = await _context.ApartImage
                .Include(a => a.Apartment)
                .SingleOrDefaultAsync(m => m.ImageId == id);
            if (apartImage == null)
            {
                return NotFound();
            }

            return View(apartImage);
        }

        // POST: ApartImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var apartImage = await _context.ApartImage.SingleOrDefaultAsync(m => m.ImageId == id);
            _context.ApartImage.Remove(apartImage);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ApartImageExists(int id)
        {
            return _context.ApartImage.Any(e => e.ImageId == id);
        }
    }
}
