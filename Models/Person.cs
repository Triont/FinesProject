using System;
using System.Collections.Generic;

#nullable disable

namespace Project5.Model
{
    public partial class Person
    {
        public Person()
        {
            Fines = new HashSet<Fine>();
            PersonCars = new HashSet<PersonCar>();
            Registrators = new HashSet<Registrator>();
        }

        public long Id { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public long? PersonCarsId { get; set; }

        public virtual ICollection<Fine> Fines { get; set; }
        public virtual ICollection<PersonCar> PersonCars { get; set; }
        public virtual ICollection<Registrator> Registrators { get; set; }
    }
}
