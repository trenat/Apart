using System;
using System.Collections.Generic;

namespace test3.Data
{
    public partial class ApartImage
    {
        public int ImageId { get; set; }
        public int ApartmentId { get; set; }
        public string ImagePath { get; set; }

        public virtual Apartment Apartment { get; set; }
    }
}
