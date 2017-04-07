using System;
using System.Collections.Generic;

namespace CoreNg2.Models
{
    public partial class Assets
    {
        public Assets()
        {
            Fields = new HashSet<Fields>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Fields> Fields { get; set; }
    }
}
