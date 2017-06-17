using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test3.Data;

namespace test3.Models.ManageViewModels
{
    public class CreateApartmentViewModel
    {
        public Apartment Apartment { get; set; }
        public IList<Opt> Options { get; set; }
    }

    public class Opt
    {
        public string Name { get; set; }

        public bool isChecked
        {
            get;
            set;
        }

    }
}
