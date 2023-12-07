namespace Theseus.Domain.CommandInterfaces.ExamCommandInterfaces
{
    public interface IDeleteExamCommand
    {
        Task Delete(Guid examId);
    }
}