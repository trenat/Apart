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
            int maxApartId = _context.Apartment.Max(apart => apart.ApartmentId);
            Random rand = new Random((int)DateTime.Now.Ticks);
            HashSet<int> numbers = new HashSet<int>();
            while (numbers.Count < 4)
            {
                numbers.Add(rand.Next(1, maxApartId + 1));
            }

            var d = _context.Apartment
                .Join(_context.ApartImage,
                    apart => apart.ApartmentId,
                    image => image.ApartmentId,
                    (apart, image) => new {Apartment = apart, ApartImage = image})              //połącz tabele apartament z tabelą obrazki apartamentów
                     .Where(result => (numbers.Contains(result.Apartment.ApartmentId)) &&       //wybierz tylko te apartamenty które zostały wylosowane (4 apartamenty)
                                 (result.ApartImage == _context.ApartImage                          
                                 .Where(r => r.ApartmentId.Equals(result.ApartImage.ApartmentId))   //Dla każdego apartamentu
                                 .OrderBy(x => Guid.NewGuid())                                       //Przesortuj losowo obrazki
                                 .FirstOrDefault()                                                   //i weź pierwszy
                                 ))
                                 .Select(x => new HomeViewModel()                                    //Wynik zapisz jako HomeViewModel
                                 {
                                    ID = x.Apartment.ApartmentId,
                                    ImagePath = x.ApartImage.ImagePath,
                                    ApartmentPrice = x.Apartment.PriceBasic,
                                 });

            
            
            return View(d);
        }

    }
}

