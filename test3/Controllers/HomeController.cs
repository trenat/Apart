using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using test3.Data;
using Microsoft.EntityFrameworkCore;
using test3.Models.HomeViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace test3.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/

        private readonly eadiApartDbContext _context;

        public HomeController(eadiApartDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var eadiApartDbContext = _context.Apartment    //From apartmnet
                .OrderBy(r => Guid.NewGuid()).Take(4)     //Take 4 random apartments 
                .Include(a => a.ApartImage); //.Take(1);               //And their photos
               // .OrderBy(r => Guid.NewGuid()).Take(1);     //Just one of course
            var xd = eadiApartDbContext.ToList();
            return View(await eadiApartDbContext.ToListAsync());
        }

    }
}
