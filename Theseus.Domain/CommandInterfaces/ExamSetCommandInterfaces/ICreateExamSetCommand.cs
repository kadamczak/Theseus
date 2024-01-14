using Theseus.Domain.Models.ExamSetRelated;

namespace Theseus.Domain.CommandInterfaces.ExamSetCommandInterfaces
{
    /// <summary>
    /// Interface defining <c>ExamSet</c> creation method.
    /// </summary>
    public interface ICreateExamSetCommand
    {
        Task CreateExamSet(ExamSet examSet);
    }
}