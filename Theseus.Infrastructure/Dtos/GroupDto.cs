using System.ComponentModel.DataAnnotations;

namespace Theseus.Infrastructure.Dtos
{
    public class GroupDto
    {
        [Key]
        public Guid Id { get; set; } = default;
        public string Name { get; set; } = default!;
        //public virtual StaffMemberDto Creator { get; set; } = default!;
        public virtual ICollection<StaffMemberDto> StaffMemberDtos { get; set; } = default!;
        public virtual ICollection<PatientDto> PatientDtos { get; set; } = default!;
        public virtual ICollection<ExamSetDto> ExamSetDtos { get; set;} = default!;
    }
}