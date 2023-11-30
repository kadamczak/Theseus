using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.StaffMemberQueries
{
    public class GetOwnerOfExamSetQuery : StaffMemberQuery, IGetOwnerOfExamSetQuery
    {
        public GetOwnerOfExamSetQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public StaffMember GetOwner(Guid examSetId)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                ExamSetDto examSetDto = context.ExamSets.Include(g => g.Owner).First(g => g.Id == examSetId);
                return GetOwner(examSetDto);
            }
        }

        public async Task<StaffMember> GetOwnerAsync(Guid examSetId)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                ExamSetDto examSetDto = await context.ExamSets.Include(g => g.Owner).FirstAsync(g => g.Id == examSetId);
                return GetOwner(examSetDto);
            }
        }

        private StaffMember GetOwner(ExamSetDto examSetDto)
        {
            StaffMemberDto owner = examSetDto.Owner;
            return MapStaffMember(owner);
        }
    }
}