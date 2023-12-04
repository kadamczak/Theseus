using Theseus.Domain.Models.ExamRelated;

namespace Theseus.Domain.CommandInterfaces.ExamCommandInterfaces
{
    public interface ICreateExamCommand
    {
        Task Create(Exam exam);
    }
}