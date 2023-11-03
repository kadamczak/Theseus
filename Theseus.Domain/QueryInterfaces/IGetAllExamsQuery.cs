using Theseus.Domain.Models.SetRelated;

namespace Theseus.Domain.QueryInterfaces
{
    public interface IGetAllExamsQuery
    {
        IEnumerable<ExamSet> GetAllExamSets();
    }
}
