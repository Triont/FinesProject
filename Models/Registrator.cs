using System;
using System.Collections.Generic;

#nullable disable

namespace Project5.Model
{
    public partial class Registrator
    {
        public Registrator()
        {
            Fines = new HashSet<Fine>();
        }

        public long Id { get; set; }
        public long? PersonId { get; set; }
        public string GerNumber { get; set; }

        public virtual Person Person { get; set; }
        public virtual ICollection<Fine> Fines { get; set; }
    }
}
