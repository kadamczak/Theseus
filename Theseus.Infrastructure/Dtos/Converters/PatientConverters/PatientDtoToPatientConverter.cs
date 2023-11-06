using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.Models.UserRelated.Enums;
using Theseus.Infrastructure.Dtos.Converters.StaffMemberConverters;

namespace Theseus.Infrastructure.Dtos.Converters.PatientConverters
{
    public class PatientDtoToPatientConverter
    {
        private readonly StaffMemberDtoToStaffMemberConverter _toStaffMemberConverter;

        public PatientDtoToPatientConverter(StaffMemberDtoToStaffMemberConverter toStaffMemberConverter)
        {
            this._toStaffMemberConverter = toStaffMemberConverter;
        }

        public Patient Convert(PatientDto patientDto)
        {
            return new Patient()
            {
                Id = patientDto.Id,
                Username = patientDto.Username,
                Age = patientDto.Age,
                Sex = (patientDto.Sex is null) ? null : Enum.Parse<Sex>(patientDto.Sex),
                ProfessionType = (patientDto.ProfessionType is null) ? null : Enum.Parse<ProfessionType>(patientDto.ProfessionType),
                EducationLevel = (patientDto.EducationLevel is null) ? null : Enum.Parse<EducationLevel>(patientDto.EducationLevel),
                DateCreated = patientDto.DateCreated,
                StaffMembers = patientDto.StaffMemberDtos.Select(_toStaffMemberConverter.Convert) as ICollection<StaffMember>
            };
        }
    }
}