using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Project5.Model
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
}
