using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project5.Models
{
    public class CarShowViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; } 
        public long OwnerId { get; set; }
        public string Fines { get; set; }
       
    }
}
