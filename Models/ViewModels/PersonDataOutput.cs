using System;
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
    public class PersonCarFineDataOutput
    {
        public long Id { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
      
      
       
        public CarDataOutput[] CarData { get; set; }
        public FineDataOutput[] FineDatas { get; set; }
    }
    public class CarDataOutput
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }

        public DateTime BeginOwning { get; set; }
        public DateTime EndOwning { get; set; }
      //  public long DriverId { get; set; }
    }
    public class FineDataOutput
    {
        public long Id { get; set; }
        public decimal Value { get; set; }

        public long? CarId { get; set; }
        public bool IsActive { get; set; }
        public string Number { get; set; }
    }
}
