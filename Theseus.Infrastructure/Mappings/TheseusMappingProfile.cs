﻿using AutoMapper;
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

            CreateMap<GroupDto, Group>().ReverseMap();

            CreateMap<ExamSetDto, ExamSet>()
                .ForMember(p => p.MazesWithSolution, c => c.MapFrom(p => p.MazeDtos))
                .ForMember(p => p.StaffMember, c => c.MapFrom(p => p.Owner));

            CreateMap<ExamSet, ExamSetDto>()
                .ForMember(p => p.MazeDtos, c => c.MapFrom(p => p.MazesWithSolution))
                .ForMember(p => p.Owner, c => c.MapFrom(p => p.StaffMember));

            CreateMap<StaffMemberDto, StaffMember>()
                .ForMember(p => p.Groups, c => c.MapFrom(p => p.GroupDtos))
                .ForMember(p => p.MazesWithSolutions, c => c.MapFrom(p => p.MazeDtos))
                .ForMember(p => p.ExamSets, c => c.MapFrom(p => p.ExamSetDtos));

            CreateMap<StaffMember, StaffMemberDto>()
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