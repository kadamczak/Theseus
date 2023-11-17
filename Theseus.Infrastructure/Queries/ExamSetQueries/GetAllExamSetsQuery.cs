using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.QueryInterfaces.ExamSetQueryInterfaces;

namespace Theseus.Infrastructure.Queries.ExamSetQueries
{
    public class GetAllExamSetsQuery : IGetAllExamsQuery
    {
        public IEnumerable<ExamSet> GetAllExamSets()
        {
            throw new NotImplementedException();
        }
    }
}
