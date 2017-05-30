using System;
using System.Collections.Generic;

namespace test3.Data
{
    public partial class Apartment
    {
        public Apartment()
        {
            ApartImage = new HashSet<ApartImage>();
            ApartOption = new HashSet<ApartOption>();
            Reservation = new HashSet<Reservation>();
        }

        public int ApartmentId { get; set; }
        public string Adress { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int OwnerId { get; set; }
        public decimal PriceBasic { get; set; }
        public int RoomSize { get; set; }

        public virtual ICollection<ApartImage> ApartImage { get; set; }
        public virtual ICollection<ApartOption> ApartOption { get; set; }
        public virtual ICollection<Reservation> Reservation { get; set; }
        public virtual User Owner { get; set; }
    }
}
