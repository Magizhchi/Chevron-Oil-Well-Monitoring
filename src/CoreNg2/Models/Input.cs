using System;
using System.Collections.Generic;

namespace CoreNg2.Models
{
    public partial class Input
    {
        public int EventId { get; set; }
        public string Tag { get; set; }
        public DateTime? Starttime { get; set; }
        public double? BaseValue { get; set; }
        public double? Variability { get; set; }
    }
}
