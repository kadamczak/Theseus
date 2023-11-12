using Theseus.Domain.Models.SetRelated;
using Theseus.Domain.QueryInterfaces.ExamQueryInterfaces;

namespace Theseus.Infrastructure.Queries.ExamQueries
{
    public class GetAllExamSetsQuery : IGetAllExamsQuery
    {
        public IEnumerable<ExamSet> GetAllExamSets()
        {
            throw new NotImplementedException();
        }
    }
}
