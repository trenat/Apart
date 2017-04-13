using System;
using System.Collections.Generic;

namespace test3.Data
{
    public partial class Reservation
    {
        public int ReservationId { get; set; }
        public int? ClientId { get; set; }
        public int? ApartmentId { get; set; }
        public string Comment { get; set; }
        public string OwnerReply { get; set; }
        public int RateId { get; set; }
        public decimal? TotalCost { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Status { get; set; }

        public virtual Apartment Apartment { get; set; }
        public virtual User Client { get; set; }
        public virtual Rate Rate { get; set; }
    }
}
