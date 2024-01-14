using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.QueryInterfaces.ExamSetQueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.ExamSetQueries
{
    /// <summary>
    /// Class defining retrieval of <c>ExamSet</c>s from a specified <c>Group</c>,
    /// using Entity Framework and <c>TheseusDbContextFactory</c>.
    /// </summary>
    public class GetExamSetsOfGroupQuery : ExamSetQuery, IGetExamSetsOfGroupQuery
    {
        public GetExamSetsOfGroupQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public IEnumerable<ExamSet> GetExamSets(Guid groupId)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                IEnumerable<ExamSetDto> examSetDtos = context.ExamSets.AsNoTracking().Where(m => m.GroupDtos.Where(g => g.Id == groupId).Any());
                return MapExamSets(examSetDtos);
            }
        }
    }
}