using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.Models.UserRelated.Enums;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.DbContexts
{
    public class TheseusDbContext : DbContext
    {
        public TheseusDbContext(DbContextOptions options) : base(options) { }

        public DbSet<MazeDto> Mazes { get; set; }
        public DbSet<ExamSetDto> ExamSets { get; set; }
        public DbSet<StaffMember> StaffMembers { get; set; }
        public DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Patient>()
                .Property(d => d.ProfessionType)
                .HasConversion(new EnumToStringConverter<ProfessionType>());

            modelBuilder
                .Entity<Patient>()
                .Property(d => d.EducationLevel)
                .HasConversion(new EnumToStringConverter<EducationLevel>());

            modelBuilder
                .Entity<Patient>()
                .Property(d => d.Sex)
                .HasConversion(new EnumToStringConverter<Sex>());
        }
    }
}