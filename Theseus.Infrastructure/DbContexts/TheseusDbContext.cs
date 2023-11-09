using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection.Metadata;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.Models.UserRelated.Enums;
using Theseus.Infrastructure.Dtos;
using Theseus.Infrastructure.Dtos.Converters;

namespace Theseus.Infrastructure.DbContexts
{
    public class TheseusDbContext : DbContext
    {
        public TheseusDbContext(DbContextOptions options) : base(options) { }

        public DbSet<MazeDto> Mazes { get; set; }
        public DbSet<ExamSetDto> ExamSets { get; set; }
        public DbSet<StaffMemberDto> StaffMembers { get; set; }
        public DbSet<PatientDto> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {



            modelBuilder.Entity<MazeDto>()
                        .HasMany(m => m.ExamSetDtos)
                        .WithMany(e => e.MazeDtos)
                        .UsingEntity<Dictionary<string, object>>(
                            "ExamSetDto_MazeDto",
                            j => j
                                .HasOne<ExamSetDto>()
                                .WithMany()
                                .OnDelete(DeleteBehavior.NoAction),
                            j => j
                                .HasOne<MazeDto>()
                                .WithMany()
                                .OnDelete(DeleteBehavior.NoAction)
                        );

            modelBuilder.Entity<PatientDto>()
                        .HasMany(m => m.StaffMemberDtos)
                        .WithMany(e => e.PatientDtos)
                        .UsingEntity<Dictionary<string, object>>(
                            "StaffMemberDto_PatientDto",
                            j => j
                                .HasOne<StaffMemberDto>()
                                .WithMany()
                                .OnDelete(DeleteBehavior.NoAction),
                            j => j
                                .HasOne<PatientDto>()
                                .WithMany()
                                .OnDelete(DeleteBehavior.NoAction)
                        );


            base.OnModelCreating(modelBuilder);
        }
    }
}