using AutoMapper;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.UserRelated;
using Theseus.Infrastructure.Dtos;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Infrastructure.Mappings.Converters.MazeConverters;

namespace Theseus.Infrastructure.Mappings
{
    /// <summary>
    /// Class defining IMapper configuration for Theseus database.
    /// </summary>
    public class TheseusMappingProfile : Profile
    {
        public TheseusMappingProfile()
        {
            CreateMap<MazeDto, MazeWithSolution>()
                .ConvertUsing(new MazeDtoToMazeWithSolutionConverter());

            CreateMap<MazeWithSolution, MazeDto>()
                .ConvertUsing(new MazeWithSolutionToMazeDtoConverter());

            CreateMap<ExamSetDto_MazeDto, ExamSetMazeIndex>()
                .ForMember(p => p.ExamSet, c => c.MapFrom(p => p.ExamSetDto))
                .ForMember(p => p.MazeWithSolution, c => c.MapFrom(p => p.MazeDto));

            CreateMap<ExamSetMazeIndex, ExamSetDto_MazeDto>()
                .ForMember(p => p.ExamSetDto, c => c.MapFrom(p => p.ExamSet))
                .ForMember(p => p.MazeDto, c => c.MapFrom(p => p.MazeWithSolution));

            CreateMap<GroupDto, Group>()
                .ForMember(p => p.Owner, c => c.MapFrom(p => p.Owner))
                .ForMember(p => p.StaffMembers, c => c.MapFrom(p => p.StaffMemberDtos))
                .ForMember(p => p.Patients, c => c.MapFrom(p => p.PatientDtos))
                .ForMember(p => p.ExamSets, c => c.MapFrom(p => p.ExamSetDtos));

            CreateMap<Group, GroupDto>()
                .ForMember(p => p.Owner, c => c.MapFrom(p => p.Owner))
                .ForMember(p => p.StaffMemberDtos, c => c.MapFrom(p => p.StaffMembers))
                .ForMember(p => p.PatientDtos, c => c.MapFrom(p => p.Patients))
                .ForMember(p => p.ExamSetDtos, c => c.MapFrom(p => p.ExamSets));

            CreateMap<ExamSetDto, ExamSet>()
               .ForMember(p => p.ExamSetMazeIndexes, c => c.MapFrom(p => p.ExamSetDto_MazeDto))
               .ForMember(p => p.Owner, c => c.MapFrom(p => p.Owner))
               .ForMember(p => p.Exams, c => c.MapFrom(c => c.ExamDtos))
               .ForMember(p => p.Groups, c => c.MapFrom(p => p.GroupDtos));

            CreateMap<ExamSet, ExamSetDto>()
                .ForMember(p => p.ExamSetDto_MazeDto, c => c.MapFrom(p => p.ExamSetMazeIndexes))
                .ForMember(p => p.Owner, c => c.MapFrom(p => p.Owner))
                .ForMember(p => p.ExamDtos, c => c.MapFrom(p => p.Exams))
                .ForMember(p => p.GroupDtos, c => c.MapFrom(p => p.Groups));

            CreateMap<StaffMemberDto, StaffMember>()
                .ForMember(p => p.OwnedGroups, c => c.MapFrom(p => p.OwnedGroupDtos))
                .ForMember(p => p.Groups, c => c.MapFrom(p => p.GroupDtos))
                .ForMember(p => p.MazesWithSolution, c => c.MapFrom(p => p.MazeDtos))
                .ForMember(p => p.ExamSets, c => c.MapFrom(p => p.ExamSetDtos));

            CreateMap<StaffMember, StaffMemberDto>()
                .ForMember(p => p.OwnedGroupDtos, c => c.MapFrom(p => p.OwnedGroups))
                .ForMember(p => p.GroupDtos, c => c.MapFrom(p => p.Groups))
                .ForMember(p => p.MazeDtos, c => c.MapFrom(p => p.MazesWithSolution))
                .ForMember(p => p.ExamSetDtos, c => c.MapFrom(p => p.ExamSets));

            CreateMap<PatientDto, Patient>()
                .ForMember(p => p.Group, c => c.MapFrom(p => p.GroupDto))
                .ForMember(p => p.Exams, c => c.MapFrom(p => p.ExamDtos));

            CreateMap<Patient, PatientDto>()
                .ForMember(p => p.GroupDto, c => c.MapFrom(p => p.Group))
                .ForMember(p => p.ExamDtos, c => c.MapFrom(p => p.Exams));

            CreateMap<Exam, ExamDto>()
                .ForMember(p => p.ExamSetDto, c => c.MapFrom(p => p.ExamSet))
                .ForMember(p => p.PatientDto, c => c.MapFrom(p => p.Patient))
                .ForMember(p => p.StageDtos, c => c.MapFrom(p => p.Stages));

            CreateMap<ExamDto, Exam>()
                .ForMember(p => p.ExamSet, c => c.MapFrom(p => p.ExamSetDto))
                .ForMember(p => p.Patient, c => c.MapFrom(p => p.PatientDto))
                .ForMember(p => p.Stages, c => c.MapFrom(p => p.StageDtos));

            CreateMap<ExamStage, ExamStageDto>()
                .ForMember(p => p.ExamDto, c => c.MapFrom(p => p.Exam))
                .ForMember(p => p.StepDtos, c => c.MapFrom(p => p.Steps));

            CreateMap<ExamStageDto, ExamStage>()
                .ForMember(p => p.Exam, c => c.MapFrom(p => p.ExamDto))
                .ForMember(p => p.Steps, c => c.MapFrom(p => p.StepDtos));

            CreateMap<ExamStep, ExamStepDto>()
                .ForMember(p => p.StageDto, c => c.MapFrom(p => p.Stage));

            CreateMap<ExamStepDto, ExamStep>()
                .ForMember(p => p.Stage, c => c.MapFrom(p => p.StageDto));
        }
    }
}