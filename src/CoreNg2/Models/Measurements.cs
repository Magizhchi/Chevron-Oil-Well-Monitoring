using System;
using System.Collections.Generic;

namespace CoreNg2.Models
{
    public partial class Measurements
    {
        public string Name { get; set; }
        public int FkWellsId { get; set; }
        public int GreaterThan { get; set; }
        public bool GreaterThanActive { get; set; }
        public int Id { get; set; }
        public string TagName { get; set; }

        public virtual Wells FkWells { get; set; }
    }
}
