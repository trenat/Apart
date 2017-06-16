using System;
using System.Collections.Generic;

namespace test3.Data
{
    public partial class User
    {
        public User()
        {
            Apartment = new HashSet<Apartment>();
            Reservation = new HashSet<Reservation>();
        }

        public int UserId { get; set; }
        public bool? Admin { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string EmailNormalized { get; set; }
        public string HashSalt { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Surname { get; set; }

        public virtual ICollection<Apartment> Apartment { get; set; }
        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
