using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project5.Models
{
    public class FineCarInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public decimal Value { get; set; }
        public DateTime DateTimeAccident { get; set; }
        public bool IsActive { get; set; }
    }
}
