using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BJJTrainer.Models
{
    public class TypeTime
    {
        public string Type { get; set; }
        public int Time { get; set; }
        public TypeTime(string type, int time)
        {
            Type = type;
            Time = time;
        }
    }
}
