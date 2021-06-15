using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project5.Models
{
    public class PersonUpdateData
    {
        public long Id { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int CarsCount { get; set; }
        public int FinesCount { get; set; }


    }
}
