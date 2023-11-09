using AutoMapper;
using Theseus.Domain.Models.UserRelated;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Mappings
{
    public class TheseusMappingProfile : Profile
    {
        public TheseusMappingProfile()
        {
            CreateMap<StaffMemberDto, StaffMember>()
                .ForMember(p => p.Patients, c => c.MapFrom(p => p.PatientDtos));

            CreateMap<StaffMember, StaffMemberDto>()
                .ForMember(p => p.PatientDtos, c => c.MapFrom(p => p.Patients));

            CreateMap<PatientDto, Patient>()
                .ForMember(p => p.StaffMembers, c => c.MapFrom(p => p.StaffMemberDtos));

            CreateMap<Patient, PatientDto>()
                .ForMember(p => p.StaffMemberDtos, c => c.MapFrom(p => p.StaffMembers));
            //CreateMap<StaffMemberDto, StaffMember>().ReverseMap();
            //CreateMap<ExamSetDto, ExamSet>().ReverseMap();


        }
    }
}