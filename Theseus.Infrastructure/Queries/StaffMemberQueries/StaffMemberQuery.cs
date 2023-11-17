using AutoMapper;
using Theseus.Domain.Models.UserRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.StaffMemberQueries
{
    public abstract class StaffMemberQuery
    {
        protected TheseusDbContextFactory DbContextFactory { get; }
        protected IMapper Mapper { get; }

        public StaffMemberQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper)
        {
            DbContextFactory = dbContextFactory;
            Mapper = mapper;
        }

        protected List<StaffMember> GetStaffMembers(TheseusDbContext context, IEnumerable<StaffMemberDto> staffMemberDtos, bool loadExamSets, bool loadPatients, bool loadMazes)
        {
            List<StaffMember> staffMembers = new List<StaffMember>();
            foreach (var staffMemberDto in staffMemberDtos)
            {
                staffMembers.Add(GetStaffMember(context, staffMemberDto, loadExamSets, loadPatients, loadMazes));
            }
            return staffMembers;
        }

        protected StaffMember GetStaffMember(TheseusDbContext context, StaffMemberDto staffMemberDto, bool loadExamSets, bool loadPatients, bool loadMazes)
        {
            if (loadExamSets)
                context.Entry(staffMemberDto).Collection(p => p.ExamSetDtos).Load();

            if (loadPatients)
                context.Entry(staffMemberDto).Collection(p => p.PatientDtos).Load();

            if (loadMazes)
                context.Entry(staffMemberDto).Collection(p => p.MazeDtos).Load();

            return MapToStaffMember(staffMemberDto);
        }

        private StaffMember MapToStaffMember(StaffMemberDto staffMemberDto) => Mapper.Map<StaffMember>(staffMemberDto);
    }
}