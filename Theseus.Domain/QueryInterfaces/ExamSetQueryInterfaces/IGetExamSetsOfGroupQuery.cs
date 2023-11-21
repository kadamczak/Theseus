using Theseus.Domain.Models.ExamSetRelated;

namespace Theseus.Domain.QueryInterfaces.ExamSetQueryInterfaces
{
    public interface IGetExamSetsOfGroupQuery
    {
        public IEnumerable<ExamSet> GetExamSets(Guid groupId);
    }
}