using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BJJTrainer.Models;

namespace BJJTrainer.Migrations
{
    [DbContext(typeof(BJJTrainerContext))]
    [Migration("20160920170905_NewModels")]
    partial class NewModels
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BJJTrainer.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("BJJTrainer.Models.Position", b =>
                {
                    b.Property<int>("PositionId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("PositionId");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("BJJTrainer.Models.Technique", b =>
                {
                    b.Property<int>("TechniqueId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<string>("Name");

                    b.Property<int>("PositionId");

                    b.HasKey("TechniqueId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("PositionId");

                    b.ToTable("Techniques");
                });

            modelBuilder.Entity("BJJTrainer.Models.Technique", b =>
                {
                    b.HasOne("BJJTrainer.Models.Category", "Category")
                        .WithMany("Techniques")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BJJTrainer.Models.Position", "Position")
                        .WithMany("Techniques")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
