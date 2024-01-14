using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.QueryInterfaces.ExamSetQueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.ExamSetQueries
{
    /// <summary>
    /// Class defining retrieval of <c>ExamSet</c>s belonging to a specified <c>StaffMember</c> that are included in specified <c>Group</c>,
    /// using Entity Framework and <c>TheseusDbContextFactory</c>.
    /// </summary>
    public class GetExamSetsOfStaffMemberInGroupQuery : ExamSetQuery, IGetExamSetsOfStaffMemberInGroupQuery
    {
        public GetExamSetsOfStaffMemberInGroupQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public IEnumerable<ExamSet> GetExamSets(Guid staffMemberId, Guid groupId)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                IEnumerable<ExamSetDto> examSetDtos = context.ExamSets
                                                             .Where(m => m.Owner.Id == staffMemberId && m.GroupDtos.Where(g => g.Id == groupId).Any())
                                                             .AsNoTracking();
                return MapExamSets(examSetDtos);
            }
        }
    }
}
