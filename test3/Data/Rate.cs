using System;
using System.Collections.Generic;

namespace test3.Data
{
    public partial class Rate
    {
        public Rate()
        {
            Reservation = new HashSet<Reservation>();
        }

        public int RateId { get; set; }
        public decimal RateLevel { get; set; }

        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
