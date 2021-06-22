using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project5.Models
{
    public class FinePersonInputData
    {
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public string CarNumber { get; set; }
        public string NumberRegistrator { get; set; }
    }

    public class FineCarInputData
    {
        public decimal Value { get; set; }
        public DateTime DateTime { get; set; }
        public string CreatedBy { get; set; }
    }
}
