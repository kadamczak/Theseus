namespace Theseus.Domain.CommandInterfaces.ExamSetCommandInterfaces
{
    public interface IRemoveExamSetCommand
    {
        Task Remove(Guid examSetId);
    }
}