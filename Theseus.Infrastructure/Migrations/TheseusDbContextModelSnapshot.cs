﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Theseus.Infrastructure.DbContexts;

#nullable disable

namespace Theseus.Infrastructure.Migrations
{
    [DbContext(typeof(TheseusDbContext))]
    partial class TheseusDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ExamSetDto_MazeDto", b =>
                {
                    b.Property<Guid>("ExamSetDtosId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MazeDtosId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ExamSetDtosId", "MazeDtosId");

                    b.HasIndex("MazeDtosId");

                    b.ToTable("ExamSetDto_MazeDto");
                });

            modelBuilder.Entity("ExamSet_Group", b =>
                {
                    b.Property<Guid>("ExamSetDtosId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GroupDtosId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ExamSetDtosId", "GroupDtosId");

                    b.HasIndex("GroupDtosId");

                    b.ToTable("ExamSet_Group");
                });

            modelBuilder.Entity("StaffMember_Group", b =>
                {
                    b.Property<Guid>("GroupDtosId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StaffMemberDtosId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GroupDtosId", "StaffMemberDtosId");

                    b.HasIndex("StaffMemberDtosId");

                    b.ToTable("StaffMember_Group");
                });

            modelBuilder.Entity("Theseus.Infrastructure.Dtos.ExamSetDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("ExamSets");
                });

            modelBuilder.Entity("Theseus.Infrastructure.Dtos.GroupDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Default")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Theseus.Infrastructure.Dtos.MazeDto", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("EndDirection")
                        .HasColumnType("tinyint");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Solution")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("SolutionStartColumn")
                        .HasColumnType("int");

                    b.Property<int>("SolutionStartRow")
                        .HasColumnType("int");

                    b.Property<byte>("StartDirection")
                        .HasColumnType("tinyint");

                    b.Property<byte[]>("Structure")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("Width")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Mazes");
                });

            modelBuilder.Entity("Theseus.Infrastructure.Dtos.PatientDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("EducationLevel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("GroupDtoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProfessionType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GroupDtoId");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Theseus.Infrastructure.Dtos.StaffMemberDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StaffMembers");
                });

            modelBuilder.Entity("ExamSetDto_MazeDto", b =>
                {
                    b.HasOne("Theseus.Infrastructure.Dtos.ExamSetDto", null)
                        .WithMany()
                        .HasForeignKey("ExamSetDtosId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Theseus.Infrastructure.Dtos.MazeDto", null)
                        .WithMany()
                        .HasForeignKey("MazeDtosId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("ExamSet_Group", b =>
                {
                    b.HasOne("Theseus.Infrastructure.Dtos.ExamSetDto", null)
                        .WithMany()
                        .HasForeignKey("ExamSetDtosId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Theseus.Infrastructure.Dtos.GroupDto", null)
                        .WithMany()
                        .HasForeignKey("GroupDtosId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("StaffMember_Group", b =>
                {
                    b.HasOne("Theseus.Infrastructure.Dtos.GroupDto", null)
                        .WithMany()
                        .HasForeignKey("GroupDtosId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Theseus.Infrastructure.Dtos.StaffMemberDto", null)
                        .WithMany()
                        .HasForeignKey("StaffMemberDtosId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Theseus.Infrastructure.Dtos.ExamSetDto", b =>
                {
                    b.HasOne("Theseus.Infrastructure.Dtos.StaffMemberDto", "Owner")
                        .WithMany("ExamSetDtos")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Theseus.Infrastructure.Dtos.GroupDto", b =>
                {
                    b.HasOne("Theseus.Infrastructure.Dtos.StaffMemberDto", "Owner")
                        .WithMany("OwnedGroupDtos")
                        .HasForeignKey("OwnerId");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Theseus.Infrastructure.Dtos.MazeDto", b =>
                {
                    b.HasOne("Theseus.Infrastructure.Dtos.StaffMemberDto", "Owner")
                        .WithMany("MazeDtos")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Theseus.Infrastructure.Dtos.PatientDto", b =>
                {
                    b.HasOne("Theseus.Infrastructure.Dtos.GroupDto", "GroupDto")
                        .WithMany("PatientDtos")
                        .HasForeignKey("GroupDtoId");

                    b.Navigation("GroupDto");
                });

            modelBuilder.Entity("Theseus.Infrastructure.Dtos.GroupDto", b =>
                {
                    b.Navigation("PatientDtos");
                });

            modelBuilder.Entity("Theseus.Infrastructure.Dtos.StaffMemberDto", b =>
                {
                    b.Navigation("ExamSetDtos");

                    b.Navigation("MazeDtos");

                    b.Navigation("OwnedGroupDtos");
                });
#pragma warning restore 612, 618
        }
    }
}
