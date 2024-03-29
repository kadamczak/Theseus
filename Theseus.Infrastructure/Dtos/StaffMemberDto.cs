﻿using System.ComponentModel.DataAnnotations;

namespace Theseus.Infrastructure.Dtos
{
    /// <summary>
    /// Class representing <c>StaffMember</c> structure as a database entry.
    /// </summary>
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

        public virtual ICollection<GroupDto> OwnedGroupDtos { get; set; } = default!;
        public virtual ICollection<GroupDto> GroupDtos { get; set; } = default!;
        public virtual ICollection<MazeDto> MazeDtos { get; set; } = default!;
        public virtual ICollection<ExamSetDto> ExamSetDtos { get; set; } = default!;
    }
}