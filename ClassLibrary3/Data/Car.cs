using System;
using System.Collections.Generic;

#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibrary1.Data
{
    public partial class Car
    {
        public Car()
        {
            Fines = new HashSet<Fine>();
            PersonCars = new HashSet<PersonCar>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }

        public virtual ICollection<Fine> Fines { get; set; }
        public virtual ICollection<PersonCar> PersonCars { get; set; }
    }
}
