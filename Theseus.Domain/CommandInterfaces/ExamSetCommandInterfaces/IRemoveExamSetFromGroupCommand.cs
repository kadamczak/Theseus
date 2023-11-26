using Theseus.Domain.Models.ExamSetRelated;

namespace Theseus.Domain.CommandInterfaces.ExamSetCommandInterfaces
{
    public interface IRemoveExamSetFromGroupCommand
    {
        Task RemoveFromGroup(ExamSet examSet, Guid groupId);
    }
}