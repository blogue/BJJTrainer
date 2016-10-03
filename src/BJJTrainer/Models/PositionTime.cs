using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BJJTrainer.Models
{
    public class PositionTime
    {
        public string Position { get; set; }
        public int Time { get; set; }
        public PositionTime(string position, int time)
        {
            Position = position;
            Time = time;
        }
    }
}
