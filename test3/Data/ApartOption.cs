using System;
using System.Collections.Generic;

namespace test3.Data
{
    public partial class ApartOption
    {
        public int ApartOptionsId { get; set; }
        public int OptionId { get; set; }
        public int ApartmentId { get; set; }

        public virtual Apartment Apartment { get; set; }
        public virtual Option Option { get; set; }
    }
}
