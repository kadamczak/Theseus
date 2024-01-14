namespace Theseus.Domain.CommandInterfaces.ExamCommandInterfaces
{
    /// <summary>
    /// Interface defining <c>Exam</c> deletion method.
    /// </summary>
    public interface IDeleteExamCommand
    {
        Task Delete(Guid examId);
    }
}