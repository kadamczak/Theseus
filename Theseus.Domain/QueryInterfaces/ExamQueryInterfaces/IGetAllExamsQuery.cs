using Theseus.Domain.Models.SetRelated;

namespace Theseus.Domain.QueryInterfaces.ExamQueryInterfaces
{
    public interface IGetAllExamsQuery
    {
        IEnumerable<ExamSet> GetAllExamSets();
    }
}
