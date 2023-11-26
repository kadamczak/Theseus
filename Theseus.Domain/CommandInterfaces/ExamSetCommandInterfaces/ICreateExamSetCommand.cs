using Theseus.Domain.Models.ExamSetRelated;

namespace Theseus.Domain.CommandInterfaces.ExamSetCommandInterfaces
{
    public interface ICreateExamSetCommand
    {
        Task CreateExamSet(ExamSet examSet);
    }
}