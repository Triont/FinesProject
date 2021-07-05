using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ClassLibrary1.Data
{
    public partial class Registrator
    {
        public Registrator()
        {
            Fines = new HashSet<Fine>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public long? PersonId { get; set; }
        public string GerNumber { get; set; }

        public virtual Person Person { get; set; }
        public virtual ICollection<Fine> Fines { get; set; }
    }


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
    public class Search
    {
        public string Data { get; set; }

    }
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
    public class FinesInputViewModel
    {
        public bool IsPersonal { get; set; }
        public decimal Value { get; set; }
        public DateTime DateTime { get; set; }
        public string NumberRegistrator { get; set; }
        public string CarNumber { get; set; }


    }
    public class ChangeCarOwnerData
    {

        public long Id { get; set; }
        public DateTime Date { get; set; }

    }
    public class CarShowViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public long OwnerId { get; set; }
        public string Fines { get; set; }

    }
    public class FineCarInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public decimal Value { get; set; }
        public DateTime DateTimeAccident { get; set; }
        public bool IsActive { get; set; }
    }
    public class FineByCarInfo
    {
        public long Id { get; set; }
        public long PersonId { get; set; }
        public string City { get; set; }
        public decimal Value { get; set; }
        public DateTime DateTimeAccident { get; set; }
        public bool IsActive { get; set; }
    }
    public class CarDataInput
    {
        public string name { get; set; }
        public string number { get; set; }
        public DateTime date { get; set; }
    }
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
