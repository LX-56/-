using System;
using System.Collections.Generic;

namespace AutomobileSalesSystem.Entitys
{
    public partial class Drivetest
    {
        public int Did { get; set; }
        public int Cid { get; set; }
        public int Aid { get; set; }
        public DateTime? Testtime { get; set; }
        public bool Active { get; set; }

        public virtual Car A { get; set; }
        public virtual Client C { get; set; }
    }
}
