using AutoMapper;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.UserRelated;
using Theseus.Infrastructure.Dtos;
using Theseus.Infrastructure.Dtos.Converters.MazeConverters;
using Theseus.Domain.Models.GroupRelated;

namespace Theseus.Infrastructure.Mappings
{
    public class TheseusMappingProfile : Profile
    {
        public TheseusMappingProfile()
        {
            CreateMap<MazeDto, MazeWithSolution>()
                .ConvertUsing(new MazeDtoToMazeWithSolutionConverter());

            CreateMap<MazeWithSolution, MazeDto>()
                .ConvertUsing(new MazeWithSolutionToMazeDtoConverter());

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
                .ForMember(p => p.MazesWithSolution, c => c.MapFrom(p => p.MazeDtos))
                .ForMember(p => p.StaffMember, c => c.MapFrom(p => p.Owner))
                .ForMember(p => p.Groups, c => c.MapFrom(p => p.GroupDtos));

            CreateMap<ExamSet, ExamSetDto>()
                .ForMember(p => p.MazeDtos, c => c.MapFrom(p => p.MazesWithSolution))
                .ForMember(p => p.Owner, c => c.MapFrom(p => p.StaffMember))
                .ForMember(p => p.GroupDtos, c => c.MapFrom(p => p.Groups));

            CreateMap<StaffMemberDto, StaffMember>()
                .ForMember(p => p.OwnedGroups, c => c.MapFrom(p => p.OwnedGroupDtos))
                .ForMember(p => p.Groups, c => c.MapFrom(p => p.GroupDtos))
                .ForMember(p => p.MazesWithSolutions, c => c.MapFrom(p => p.MazeDtos))
                .ForMember(p => p.ExamSets, c => c.MapFrom(p => p.ExamSetDtos));

            CreateMap<StaffMember, StaffMemberDto>()
                .ForMember(p => p.OwnedGroupDtos, c => c.MapFrom(p => p.OwnedGroups))
                .ForMember(p => p.GroupDtos, c => c.MapFrom(p => p.Groups))
                .ForMember(p => p.MazeDtos, c => c.MapFrom(p => p.MazesWithSolutions))
                .ForMember(p => p.ExamSetDtos, c => c.MapFrom(p => p.ExamSets));

            CreateMap<PatientDto, Patient>()
                .ForMember(p => p.Group, c => c.MapFrom(p => p.GroupDto));

            CreateMap<Patient, PatientDto>()
                .ForMember(p => p.GroupDto, c => c.MapFrom(p => p.Group));
            
        }
    }
}