using System;
using System.Collections.Generic;

#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project5.Model
{
    public partial class Fine
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateTmeOfAccident { get; set; }
        public decimal Value { get; set; }
        public long RegistratorId { get; set; }
        public long? CarId { get; set; }
        public long? DriverId { get; set; }
        public bool IsPersonal { get; set; }

        public virtual Car Car { get; set; }
        public virtual Person Driver { get; set; }
        public virtual Registrator Registrator { get; set; }
    }
}
