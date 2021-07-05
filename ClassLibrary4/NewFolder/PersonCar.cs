using System;
using System.Collections.Generic;

#nullable disable

namespace ClassLibrary4.Model
{
    public partial class PersonCar
    {
        public long CarId { get; set; }
        public long PersonId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public virtual Car Car { get; set; }
        public virtual Person Person { get; set; }
    }
}
