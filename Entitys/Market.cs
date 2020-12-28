using System;
using System.Collections.Generic;

namespace AutomobileSalesSystem.Entitys
{
    public partial class Market
    {
        public int Id { get; set; }
        public int Cid { get; set; }
        public int Sid { get; set; }
        public int Aid { get; set; }
        public DateTime Saledatatime { get; set; }

        public virtual Car A { get; set; }
        public virtual Client C { get; set; }
        public virtual Salesman S { get; set; }
    }
}
