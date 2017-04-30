using System;
using System.Collections.Generic;

namespace CoreNg2.Models
{
    public partial class History
    {
        public string Tag { get; set; }
        public DateTime Time { get; set; }
        public double Value { get; set; }
    }
}
