using Theseus.Domain.Models.ExamRelated;

namespace Theseus.Domain.CommandInterfaces.ExamCommandInterfaces
{
    public interface ICreateExamCommand
    {
        void Create(Exam exam);
    }
}