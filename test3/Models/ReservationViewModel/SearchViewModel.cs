using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using test3.Data;

namespace test3.Models.ReservationViewModel
{
    public class SearchViewModel
    {
        public decimal PriceMin { get; set; }
        public decimal PriceMax { get; set; }
        [InYearRange]
        public DateTime DateFrom { get; set; } = DateTime.Now;
        [InYearRange]
        public DateTime DateTo { get; set; } = DateTime.Now;

        public int RateMin { get; set; } 
        public int RateMax { get; set; } = 5;
        public bool UsePrice { get; set; }
        public bool UseDate { get; set; }
        public bool UseRate { get; set; }
        public IList<Apartment> Apartments { get; set; }
        public IList<IsOption> Options { get; set; }
        
    }

    public class CreateViewModel
    {
        public DateTime DateFrom { set; get; }
        public DateTime DateTo { set; get; }
        public int ApartId { set; get; }

    }

    public class IsOption
    {
        public bool IsSet { get; set; }
        public string Name { get; set; }
    }

    public class InYearRange : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (DateTime.Now.CompareTo((DateTime)value) >= 0)
            {
                return new ValidationResult("Nie możesz zrobić rezerwacji w przeszłości!");
            }
            if (DateTime.Now.AddYears(1).CompareTo((DateTime) value) <= 0)
            {
                return new ValidationResult("Nie możesz zrobić rezerwacji w przód");
            }

            return ValidationResult.Success;
        }
    }
}

