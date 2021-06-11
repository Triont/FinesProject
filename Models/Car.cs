using System;
using System.Collections.Generic;

#nullable disable

namespace Project5.Model
{
    public partial class Car
    {
        public Car()
        {
            Fines = new HashSet<Fine>();
            PersonCars = new HashSet<PersonCar>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }

        public virtual ICollection<Fine> Fines { get; set; }
        public virtual ICollection<PersonCar> PersonCars { get; set; }
    }
}
