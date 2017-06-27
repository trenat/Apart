using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test3.Data;

namespace test3.Models.ReservationViewModel
{
    public class SearchViewModel
    {
        private DateTime _dateTo = DateTime.Today.Date;
        private DateTime _dateFrom = DateTime.Today.Date;
        public int PriceMin { get; set; }
        public int PriceMax { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int RateMin { get; set; }
        public int RateMax { get; set; }
        public IList<Apartment> Apartments { get; set; }
        public IList<IsOption> Options { get; set; }
        
    }

    public class IsOption
    {
        public bool isSet { get; set; }
        public string Name { get; set; }

        public IsOption(Option option)
        {
            Name = option.Name;
            isSet = true;
        }
    }
}

