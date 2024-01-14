using Theseus.Domain.Models.ExamRelated;

namespace Theseus.Domain.CommandInterfaces.ExamCommandInterfaces
{
    /// <summary>
    /// Interface defining <c>Exam</c> creation method.
    /// </summary>
    public interface ICreateExamCommand
    {
        void Create(Exam exam);
    }
}