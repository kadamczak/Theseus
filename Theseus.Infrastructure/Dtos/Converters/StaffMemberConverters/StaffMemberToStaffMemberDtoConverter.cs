using Theseus.Domain.Models.UserRelated;
using Theseus.Infrastructure.Dtos.Converters.ExamSetConverters;
using Theseus.Infrastructure.Dtos.Converters.MazeConverters;
using Theseus.Infrastructure.Dtos.Converters.PatientConverters;

namespace Theseus.Infrastructure.Dtos.Converters.StaffMemberConverters
{
    public class StaffMemberToStaffMemberDtoConverter
    {
        private readonly PatientToPatientDtoConverter _toPatientDtoConverter;
        private readonly MazeWithSolutionToMazeDtoConverter _toMazeDtoConverter;
        private readonly ExamSetToExamSetDtoConverter _toExamSetDtoConverter;

        public StaffMemberToStaffMemberDtoConverter(PatientToPatientDtoConverter toPatientDtoConverter,
                                                    MazeWithSolutionToMazeDtoConverter toMazeDtoConverter,
                                                    ExamSetToExamSetDtoConverter toExamSetDtoConverter)
        {
            this._toPatientDtoConverter = toPatientDtoConverter;
            this._toMazeDtoConverter = toMazeDtoConverter;
            this._toExamSetDtoConverter = toExamSetDtoConverter;
        }

        public StaffMemberDto Convert(StaffMember dto)
        {
            return new StaffMemberDto()
            {
                Id = dto.Id,
                Username = dto.Username,
                PasswordHash = dto.PasswordHash,
                Email = dto.Email,
                Name = dto.Name,
                Surname = dto.Surname,
                DateCreated = dto.DateCreated,
                PatientDtos = dto.Patients.Select(_toPatientDtoConverter.Convert) as ICollection<PatientDto>,
                //MazeDtos = dto.MazesWithSolutions.Select(_toMazeDtoConverter.Convert) as ICollection<MazeDto>,
                //ExamSetDtos = dto.ExamSets.Select(_toExamSetDtoConverter.Convert) as ICollection<ExamSetDto>
            };
        }
    }
}