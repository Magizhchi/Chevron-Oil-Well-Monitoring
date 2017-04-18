using System;
using System.Collections.Generic;

namespace CoreNg2.Models
{
    public partial class Rules
    {
        public Rules()
        {
            WEvents = new HashSet<WEvents>();
        }

        public int Id { get; set; }
        public int FkRuleTypeId { get; set; }
        public bool IsActive { get; set; }
        public int FkMeasurementsId { get; set; }
        public double Value { get; set; }

        public virtual ICollection<WEvents> WEvents { get; set; }
        public virtual Measurements FkMeasurements { get; set; }
    }
}
