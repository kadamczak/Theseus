using Theseus.Domain.Models.ExamSetRelated;

namespace Theseus.Domain.QueryInterfaces.ExamSetQueryInterfaces
{
    /// <summary>
    /// Interface defining retrieval of <c>ExamSet</c>s from a specified <c>Group</c>.
    /// </summary>
    public interface IGetExamSetsOfGroupQuery
    {
        public IEnumerable<ExamSet> GetExamSets(Guid groupId);
    }
}