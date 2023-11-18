using AutoMapper;
using Theseus.Domain.Models.UserRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.StaffMemberQueries
{
    public abstract class StaffMemberQuery : Query
    {
        protected StaffMemberQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        protected List<StaffMember> GetStaffMembers(TheseusDbContext context, IEnumerable<StaffMemberDto> staffMemberDtos, bool loadExamSets, bool loadGroups, bool loadMazes)
        {
            List<StaffMember> staffMembers = new List<StaffMember>();
            foreach (var staffMemberDto in staffMemberDtos)
            {
                staffMembers.Add(GetStaffMember(context, staffMemberDto, loadExamSets, loadGroups, loadMazes));
            }
            return staffMembers;
        }

        protected StaffMember GetStaffMember(TheseusDbContext context, StaffMemberDto staffMemberDto, bool loadExamSets, bool loadGroups, bool loadMazes)
        {
            if (loadExamSets)
                context.Entry(staffMemberDto).Collection(p => p.ExamSetDtos).Load();

            if (loadGroups)
                context.Entry(staffMemberDto).Collection(p => p.GroupDtos).Load();

            if (loadMazes)
                context.Entry(staffMemberDto).Collection(p => p.MazeDtos).Load();

            return MapToStaffMember(staffMemberDto);
        }

        private StaffMember MapToStaffMember(StaffMemberDto staffMemberDto) => Mapper.Map<StaffMember>(staffMemberDto);
    }
}