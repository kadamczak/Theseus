namespace Theseus.Domain.CommandInterfaces.ExamSetCommandInterfaces
{
    public interface IDeleteExamSetCommand
    {
        Task Delete(Guid examSetId);
    }
}