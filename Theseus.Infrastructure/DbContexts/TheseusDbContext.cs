using Microsoft.EntityFrameworkCore;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.DbContexts
{
    public class TheseusDbContext : DbContext
    {
        public TheseusDbContext(DbContextOptions options) : base(options) { }

        public DbSet<MazeDto> Mazes { get; set; }
        public DbSet<ExamSetDto> ExamSets { get; set; }
        public DbSet<StaffMemberDto> StaffMembers { get; set; }
        public DbSet<PatientDto> Patients { get; set; }
        public DbSet<GroupDto> Groups { get; set; }
        public DbSet<ExamSetDto_MazeDto> ExamSetDtos_MazeDtos { get; set; }

        public DbSet<ExamDto> Exams { get; set; }
        public DbSet<ExamStageDto> ExamStages { get; set; }
        public DbSet<ExamStepDto> ExamSteps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StaffMemberDto>()
                        .HasMany(m => m.GroupDtos)
                        .WithMany(e => e.StaffMemberDtos)
                        .UsingEntity<Dictionary<string, object>>(
                            "StaffMember_Group",
                            j => j
                                .HasOne<GroupDto>()
                                .WithMany()
                                .OnDelete(DeleteBehavior.NoAction),
                            j => j
                                .HasOne<StaffMemberDto>()
                                .WithMany()
                                .OnDelete(DeleteBehavior.NoAction)
                        );

            modelBuilder.Entity<ExamSetDto>()
                        .HasMany(m => m.GroupDtos)
                        .WithMany(e => e.ExamSetDtos)
                        .UsingEntity<Dictionary<string, object>>(
                            "ExamSet_Group",
                            j => j
                                .HasOne<GroupDto>()
                                .WithMany()
                                .OnDelete(DeleteBehavior.NoAction),
                            j => j
                                .HasOne<ExamSetDto>()
                                .WithMany()
                                .OnDelete(DeleteBehavior.NoAction)
                        );

            modelBuilder.Entity<StaffMemberDto>()
                        .HasMany(m => m.OwnedGroupDtos)
                        .WithOne(e => e.Owner)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ExamSetDto>()
            .HasMany(m => m.ExamSetDto_MazeDto)
            .WithOne(e => e.ExamSetDto)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MazeDto>()
                        .HasMany(m => m.ExamSetDto_MazeDto)
                        .WithOne(e => e.MazeDto)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ExamSetDto>()
                        .HasMany(m => m.ExamDtos)
                        .WithOne(e => e.ExamSet)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ExamDto>()
                        .HasMany(m => m.Stages)
                        .WithOne(e => e.Exam)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ExamStageDto>()
                        .HasMany(m => m.Steps)
                        .WithOne(e => e.Stage)
                        .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}