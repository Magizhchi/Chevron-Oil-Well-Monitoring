using System;
using System.Collections.Generic;

namespace CoreNg2.Models
{
    public partial class WEvents
    {
        public int Id { get; set; }
        public int RuleId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int MaxValue { get; set; }

        public virtual Rules Rule { get; set; }
    }
}
