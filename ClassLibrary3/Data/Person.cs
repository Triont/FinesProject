using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ClassLibrary1.Data
{

    public partial class Person
       
    {
        private Database_FinesContext Database_FinesContext;
        public Person()
        {
            Fines = new HashSet<Fine>();
            PersonCars = new HashSet<PersonCar>();
            Registrators = new HashSet<Registrator>();
        }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
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
