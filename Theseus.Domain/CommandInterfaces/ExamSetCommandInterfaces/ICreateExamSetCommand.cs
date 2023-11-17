using Theseus.Domain.Models.ExamSetRelated;

namespace Theseus.Domain.ExamSetCommandInterfaces
{
    public interface ICreateExamSetCommand
    {
        Task CreateExamSet(ExamSet examSet);
    }
}