using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.Models.SetRelated;
using Theseus.Domain.Models.UserRelated;
using Theseus.Infrastructure.Dtos.Converters.ExamSetConverters;
using Theseus.Infrastructure.Dtos.Converters.MazeConverters;
using Theseus.Infrastructure.Dtos.Converters.PatientConverters;

namespace Theseus.Infrastructure.Dtos.Converters.StaffMemberConverters
{
    public class StaffMemberDtoToStaffMemberConverter
    {
        private readonly PatientDtoToPatientConverter _toPatientConverter;
        private readonly MazeDtoToMazeWithSolutionConverter _toMazeConverter;
        private readonly ExamSetDtoToExamSetConverter _toExamSetConverter;

        public StaffMemberDtoToStaffMemberConverter(PatientDtoToPatientConverter toPatientConverter,
                                                    MazeDtoToMazeWithSolutionConverter toMazeConverter,
                                                    ExamSetDtoToExamSetConverter toExamSetConverter)
        {
            this._toPatientConverter = toPatientConverter;
            this._toMazeConverter = toMazeConverter;
            this._toExamSetConverter = toExamSetConverter;
        }

        public StaffMember Convert(StaffMemberDto dto)
        {
            return new StaffMember()
            {
                Id = dto.Id,
                Username = dto.Username,
                PasswordHash = dto.PasswordHash,
                Email = dto.Email,
                Name = dto.Name,
                Surname = dto.Surname,
                DateCreated = dto.DateCreated,
                Patients = dto.PatientDtos.Select(_toPatientConverter.Convert) as ICollection<Patient>,
                //MazesWithSolutions = dto.MazeDtos.Select(_toMazeConverter.Convert) as ICollection<MazeWithSolution>,
                //ExamSets = dto.ExamSetDtos.Select(_toExamSetConverter.Convert) as ICollection<ExamSet>
            };
        }
    }
}