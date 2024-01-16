namespace Theseus.Domain.Services.ExamDataServices.Summary.ExamSetGroup
{
    /// <summary>
    /// The <c>ExamSetGroupStatsList</c> class contains all <c>ExamSetGroupStatSummary</c> objects that were calculated during the last <c>ExamSetGroupStatCalculator</c> use.
    /// </summary>
    public class ExamSetGroupStatsList
    {
        public List<ExamSetGroupStatSummary> ExamSetStatList { get; set; } = new List<ExamSetGroupStatSummary>();
    }
}