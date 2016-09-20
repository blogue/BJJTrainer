using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BJJTrainer.Models;

namespace BJJTrainer.Models
{
    public class BJJTrainerContext : DbContext
    {
        public BJJTrainerContext (DbContextOptions<BJJTrainerContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }

        public DbSet<Position> Position { get; set; }

        public DbSet<Technique> Technique { get; set; }
    }
}
