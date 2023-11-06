using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.Models.UserRelated.Enums;
using Theseus.Infrastructure.Dtos.Converters.StaffMemberConverters;

namespace Theseus.Infrastructure.Dtos.Converters.PatientConverters
{
    public class PatientToPatientDtoConverter
    {
        private readonly StaffMemberToStaffMemberDtoConverter _toStaffMemberDtoConverter;

        public PatientToPatientDtoConverter(StaffMemberToStaffMemberDtoConverter toStaffMemberDtoConverter)
        {
            this._toStaffMemberDtoConverter = toStaffMemberDtoConverter;
        }

        public PatientDto Convert(Patient patient)
        {
            return new PatientDto()
            {
                Id = patient.Id,
                Username = patient.Username,
                Age = patient.Age,
                Sex = (patient.Sex is null) ? null : patient.Sex.ToString(),
                ProfessionType = (patient.ProfessionType is null) ? null : patient.ProfessionType.ToString(),
                EducationLevel = (patient.EducationLevel is null) ? null : patient.EducationLevel.ToString(),
                DateCreated = patient.DateCreated,
                StaffMemberDtos = patient.StaffMembers.Select(_toStaffMemberDtoConverter.Convert) as ICollection<StaffMemberDto>
            };
        }
    }
}