﻿using System.ComponentModel.DataAnnotations;

namespace Theseus.Infrastructure.Dtos
{
    public class StaffMemberDto
    {
        [Key]
        public Guid Id { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public ICollection<PatientDto> PatientDtos { get; set; } = default!;
        public ICollection<MazeDto> MazeDtos { get; set; } = default!;
        public ICollection<ExamSetDto> ExamSetDtos { get; set; } = default!;
    }
}