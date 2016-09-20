using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BJJTrainer.Models
{
    [Table("Positions")]
    public class Position
    {
        [Key]
        public int PositionId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Technique> Techniques { get; set; }
    }
}
