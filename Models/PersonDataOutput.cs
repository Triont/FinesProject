﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project5.Model;
using Project5.Models;

namespace Project5.Models
{
    public class PersonDataOutput
    {
     //   public Person Person { get; set; }
        public long Id { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string FineData { get; set; }
      public string CarData { get; set; }
       
    }
}