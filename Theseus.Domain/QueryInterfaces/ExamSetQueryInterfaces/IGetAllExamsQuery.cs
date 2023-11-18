using Theseus.Domain.Models.ExamSetRelated;

namespace Theseus.Domain.QueryInterfaces.ExamSetQueryInterfaces
{
    public interface IGetAllExamsQuery
    {
        IEnumerable<ExamSet> GetAllExamSets(bool loadOwner = false, bool loadMazes = false, bool loadGroups = false);
    }
}
