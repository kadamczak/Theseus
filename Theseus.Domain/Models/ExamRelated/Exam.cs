using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.Models.ExamRelated
{
    /// <summary>
    /// The <c>Exam</c> class represents a <c>Patient</c>'s singular attempt at solving an <c>ExamSet</c>.
    /// </summary>
    public class Exam
    {
        /// <summary>
        /// Gets or sets <c>Exam</c> ID.
        /// </summary>
        public Guid Id { get; set; } = default!;

        /// <summary>
        /// Gets or sets the <c>Patient</c> that did the <c>Exam</c>.
        /// </summary>
        public Patient Patient { get; set; } = default!;

        /// <summary>
        /// Gets or sets the <c>ExamSet</c> used for this <c>Exam</c>.
        /// </summary>
        public ExamSet ExamSet { get; set; } = default!;

        /// <summary>
        /// Gets or sets the date when <c>Exam</c> was finished.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the <c>ExamStage</c>s that the <c>Exam</c> consists of.
        /// </summary>
        public List<ExamStage> Stages { get; set; } = new List<ExamStage>();

        /// <summary>
        /// Initializes <c>Exam</c> with no additional actions.
        /// </summary>
        public Exam() { }

        /// <summary>
        /// Initializes <c>Exam</c> with ID.
        /// </summary>
        /// <param name="id"><c>Exam</c> ID.</param>
        public Exam(Guid id)
        {
            Id = id;
        }
    }
}