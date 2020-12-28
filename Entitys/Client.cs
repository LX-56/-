using System;
using System.Collections.Generic;

namespace AutomobileSalesSystem.Entitys
{
    public partial class Client
    {
        public Client()
        {
            Drivetest = new HashSet<Drivetest>();
            Market = new HashSet<Market>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Tel { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<Drivetest> Drivetest { get; set; }
        public virtual ICollection<Market> Market { get; set; }
    }
}
