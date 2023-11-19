using Theseus.Domain.Models.ExamSetRelated;

namespace Theseus.Domain.QueryInterfaces.ExamSetQueryInterfaces
{
    public interface IGetAllExamSetsOfStaffMemberQuery
    {
        IEnumerable<ExamSet> GetExamSets(Guid staffMemberId);
    }
}
