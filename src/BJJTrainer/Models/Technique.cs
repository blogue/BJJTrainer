using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BJJTrainer.Models
{
    [Table("Techniques")]
    public class Technique
    {
        [Key]
        public int TechniqueId { get; set; }
        public string Name { get; set; }
        public int PositionId { get; set; }
        public virtual Position Position { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

    }
}
