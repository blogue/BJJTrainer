using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BJJTrainer.Models
{
    [Table("Drills")]
    public class Drill
    {
        [Key]
        public int DrillId { get; set; }
        public string Description { get; set; }
        public int Time { get; set; }
        public string Video { get; set; }
        public int TechniqueId { get; set; }
        public virtual Technique Technique { get; set; }

        public int RoutineId { get; set; }
        public virtual Routine Routine { get; set; }
    }
}
