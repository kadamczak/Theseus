using AutoMapper;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.UserRelated;
using Theseus.Infrastructure.Dtos;
using Theseus.Infrastructure.Dtos.Converters.MazeConverters;

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

            CreateMap<ExamSetDto, ExamSet>()
                .ForMember(p => p.MazesWithSolution, c => c.MapFrom(p => p.MazeDtos))
                .ForMember(p => p.StaffMember, c => c.MapFrom(p => p.Owner));

            CreateMap<ExamSet, ExamSetDto>()
                .ForMember(p => p.MazeDtos, c => c.MapFrom(p => p.MazesWithSolution))
                .ForMember(p => p.Owner, c => c.MapFrom(p => p.StaffMember));

            CreateMap<StaffMemberDto, StaffMember>()
                .ForMember(p => p.Patients, c => c.MapFrom(p => p.PatientDtos))
                .ForMember(p => p.MazesWithSolutions, c => c.MapFrom(p => p.MazeDtos))
                .ForMember(p => p.ExamSets, c => c.MapFrom(p => p.ExamSetDtos));

            CreateMap<StaffMember, StaffMemberDto>()
                .ForMember(p => p.PatientDtos, c => c.MapFrom(p => p.Patients))
                .ForMember(p => p.MazeDtos, c => c.MapFrom(p => p.MazesWithSolutions))
                .ForMember(p => p.ExamSetDtos, c => c.MapFrom(p => p.ExamSets));

            CreateMap<PatientDto, Patient>()
                .ForMember(p => p.StaffMembers, c => c.MapFrom(p => p.StaffMemberDtos));

            CreateMap<Patient, PatientDto>()
                .ForMember(p => p.StaffMemberDtos, c => c.MapFrom(p => p.StaffMembers));
            
        }
    }
}