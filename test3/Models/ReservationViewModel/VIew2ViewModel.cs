using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test3.Data;

namespace test3.Models.ReservationViewModel
{
    public class View2ViewModel
    {
        public IList<Apartment> Apartments { get; set; }

        public IList<IsOption> Options { get; set; }
    }
}
