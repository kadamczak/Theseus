using AutoMapper;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.QueryInterfaces.ExamSetQueryInterfaces;
using Theseus.Infrastructure.DbContexts;

namespace Theseus.Infrastructure.Queries.ExamSetQueries
{
    public class GetAllExamSetsQuery : ExamSetQuery, IGetAllExamsQuery
    {
        public GetAllExamSetsQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public IEnumerable<ExamSet> GetAllExamSets()
        {
            throw new NotImplementedException();
        }
    }
}