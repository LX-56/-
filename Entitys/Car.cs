using System;
using System.Collections.Generic;

namespace AutomobileSalesSystem.Entitys
{
    public partial class Car
    {
        public Car()
        {
            Drivetest = new HashSet<Drivetest>();
            Market = new HashSet<Market>();
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public string Model { get; set; }
        public string Factory { get; set; }
        public DateTime Datatime { get; set; }

        public virtual ICollection<Drivetest> Drivetest { get; set; }
        public virtual ICollection<Market> Market { get; set; }
    }
}
