using System;
using System.Collections.Generic;

namespace CoreNg2.Models
{
    public partial class Wells
    {
        public Wells()
        {
            Measurements = new HashSet<Measurements>();
        }

        public int Id { get; set; }
        public int FkFieldsId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Measurements> Measurements { get; set; }
        public virtual Fields FkFields { get; set; }
    }
}
