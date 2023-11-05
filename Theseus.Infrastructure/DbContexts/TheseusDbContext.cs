using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
            //MazeWithSolutionToMazeDtoConverter toMazeDtoConverter = new MazeWithSolutionToMazeDtoConverter();
            //MazeDtoToMazeWithSolutionConverter toMazeWithSolutionConverter = new MazeDtoToMazeWithSolutionConverter();

            //modelBuilder
            //    .Entity<StaffMember>()
            //    .Property(d => d.MazesWithSolutions)
            //    .HasConversion(new CollectionConverter(toMazeDtoConverter, toMazeWithSolutionConverter));

            //modelBuilder
            //    .Entity<Patient>()
            //    .Property(d => d.ProfessionType)
            //    .HasConversion(new EnumToStringConverter<ProfessionType>());

            //modelBuilder
            //    .Entity<Patient>()
            //    .Property(d => d.EducationLevel)
            //    .HasConversion(new EnumToStringConverter<EducationLevel>());

            //modelBuilder
            //    .Entity<Patient>()
            //    .Property(d => d.Sex)
            //    .HasConversion(new EnumToStringConverter<Sex>());

            base.OnModelCreating(modelBuilder);
        }
    }
}