using Theseus.Domain.Models.ExamRelated;

namespace Theseus.Domain.QueryInterfaces.ExamQueryInterfaces
{
    /// <summary>
    /// Interface defining retrieval of <c>Exam</c>s done by specified <c>Patient</c>.
    /// Related entities are included.
    /// </summary>
    public interface IGetExamsOfPatientWithFullIncludeQuery
    {
        IEnumerable<Exam> GetExams(Guid patientId);
    }
}