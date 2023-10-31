using Theseus.Domain.Models.SetRelated;

namespace Theseus.Domain.CommandInterfaces
{
    public interface ICreateExamSetCommand
    {
        Task CreateExamSet(ExamSet examSet);
    }
}