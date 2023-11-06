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
            base.OnModelCreating(modelBuilder);
        }
    }
}