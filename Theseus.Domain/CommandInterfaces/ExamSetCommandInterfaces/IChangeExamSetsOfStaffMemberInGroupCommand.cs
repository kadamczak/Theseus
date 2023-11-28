using Theseus.Domain.Models.ExamSetRelated;

namespace Theseus.Domain.CommandInterfaces.ExamSetCommandInterfaces
{
    public interface IChangeExamSetsOfStaffMemberInGroupCommand
    {
        Task ChangeExamSets(IEnumerable<ExamSet> newExamSetCollection, Guid groupId, Guid staffMemberId);
    }
}