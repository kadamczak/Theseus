using Theseus.Domain.Models.ExamSetRelated;

namespace Theseus.Domain.CommandInterfaces.ExamSetCommandInterfaces
{
    /// <summary>
    /// Interface defining change of <c>ExamSet</c> selection by <c>StaffMember</c> in <c>Group</c>.
    /// </summary>
    public interface IChangeExamSetsOfStaffMemberInGroupCommand
    {
        Task ChangeExamSets(IEnumerable<ExamSet> newExamSetCollection, Guid groupId, Guid staffMemberId);
    }
}