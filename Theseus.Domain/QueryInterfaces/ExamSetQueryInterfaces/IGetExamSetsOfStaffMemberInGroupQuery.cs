using Theseus.Domain.Models.ExamSetRelated;

namespace Theseus.Domain.QueryInterfaces.ExamSetQueryInterfaces
{
    public interface IGetExamSetsOfStaffMemberInGroupQuery
    {
        IEnumerable<ExamSet> GetExamSets(Guid staffMemberId, Guid groupId);
    }
}