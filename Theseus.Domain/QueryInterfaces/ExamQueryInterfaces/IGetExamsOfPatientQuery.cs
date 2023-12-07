using Theseus.Domain.Models.ExamRelated;

namespace Theseus.Domain.QueryInterfaces.ExamQueryInterfaces
{
    public interface IGetExamsOfPatientQuery
    {
        IEnumerable<Exam> GetExams(Guid patientId);
    }
}