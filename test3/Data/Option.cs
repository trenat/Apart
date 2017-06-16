using System;
using System.Collections.Generic;

namespace test3.Data
{
    public partial class Option
    {
        public Option()
        {
            ApartOption = new HashSet<ApartOption>();
        }

        public int OptionId { get; set; }
        public string ImagePath { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ApartOption> ApartOption { get; set; }
    }
}
