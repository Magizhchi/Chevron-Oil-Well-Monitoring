using System;
using System.Collections.Generic;

namespace CoreNg2.Models
{
    public partial class Fields
    {
        public Fields()
        {
            Wells = new HashSet<Wells>();
        }

        public int Id { get; set; }
        public int FkAssetId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Wells> Wells { get; set; }
        public virtual Assets FkAsset { get; set; }
    }
}
