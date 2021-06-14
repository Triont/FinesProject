using System;
using System.Collections.Generic;

#nullable disable

namespace Project5.Model
{
    public partial class Fine
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateTmeOfAccident { get; set; }
        public decimal Value { get; set; }
        public long RegistratorId { get; set; }
        public long? CarId { get; set; }
        public long? DriverId { get; set; }

        public virtual Car Car { get; set; }
        public virtual Person Driver { get; set; }
        public virtual Registrator Registrator { get; set; }
    }
}
