using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using test3.Data;

namespace test3.Models.ManageViewModels
{
    public class ManageIndexViewModel
    {
        public User User { get; set; }
        public IIncludableQueryable<Apartment,Option> Apartments { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
