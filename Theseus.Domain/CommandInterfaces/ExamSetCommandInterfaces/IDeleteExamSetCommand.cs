namespace Theseus.Domain.CommandInterfaces.ExamSetCommandInterfaces
{
    /// <summary>
    /// Interface defining <c>ExamSet</c> deletion method.
    /// </summary>
    public interface IDeleteExamSetCommand
    {
        Task Delete(Guid examSetId);
    }
}