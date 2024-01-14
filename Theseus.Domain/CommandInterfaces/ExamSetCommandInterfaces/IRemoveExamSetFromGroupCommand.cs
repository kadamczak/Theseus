using Theseus.Domain.Models.ExamSetRelated;

namespace Theseus.Domain.CommandInterfaces.ExamSetCommandInterfaces
{
    /// <summary>
    /// Interface defining method of <c>ExamSet</c> removal from <c>Group</c>.
    /// </summary>
    public interface IRemoveExamSetFromGroupCommand
    {
        Task RemoveFromGroup(ExamSet examSet, Guid groupId);
    }
}