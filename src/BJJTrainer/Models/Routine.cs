using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BJJTrainer.Models
{
    [Table("Routines")]
    public class Routine
    {
        public int RoutineId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Drill> Drills { get; set; }
    }
}
