namespace Theseus.Domain.CommandInterfaces.ExamSetCommandInterfaces
{
    public interface IDeleteExamSetCommand
    {
        Task Remove(Guid examSetId);
    }
}