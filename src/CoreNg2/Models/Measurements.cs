using System;
using System.Collections.Generic;

namespace CoreNg2.Models
{
    public partial class Measurements
    {
        public Measurements()
        {
            Rules = new HashSet<Rules>();
        }

        public string Name { get; set; }
        public int FkWellsId { get; set; }
        public int Id { get; set; }
        public string TagName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Rules> Rules { get; set; }
        public virtual Wells FkWells { get; set; }
    }
}
