using System.ComponentModel.DataAnnotations;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.Models.ExamSetRelated
{
    /// <summary>
    /// The <c>ExamSet</c> class represents a collection of <c>MazeWithSolution</c> objects that are meant to be solved together by a <c>Patient</c>.
    /// </summary>
    public class ExamSet
    {
        /// <summary>
        /// Gets or sets <c>ExamSet</c> ID.
        /// </summary>
        public Guid Id { get; set; } = default;

        /// <summary>
        /// Gets or sets displayable name of <c>ExamSet</c>.
        /// </summary>
        [Required]
        [StringLength(16)]
        [RegularExpression(@"^[\w_]+$")]
        public string Name { get; set; } = default!;

        /// <summary>
        /// Gets or sets a list of <c>ExamSetMazeIndex</c> objects, which represent an <c>MazeWithSolution</c> and its order within the <c>ExamSet</c>.
        /// </summary>
        public List<ExamSetMazeIndex> ExamSetMazeIndexes { get; set; } = new List<ExamSetMazeIndex>();

        /// <summary>
        /// Gets or sets the owner of the <c>ExamSet</c>.
        /// </summary>
        public StaffMember StaffMember { get; set; } = default!;

        /// <summary>
        /// Gets or sets a list of <c>Group</c>s which <c>Patient</c>s can use this <c>ExamSet</c>.
        /// </summary>
        public List<Group> Groups { get; set; } = new List<Group>();

        /// <summary>
        /// Gets or sets a list of <c>Exam</c>s that were conducted using this <c>ExamSet</c>.
        /// </summary>
        public List<Exam> Exams { get; set; } = new List<Exam>();

        /// <summary>
        /// Initializes <c>ExamSet</c> with no additional actions.
        /// </summary>
        public ExamSet() { }

        /// <summary>
        /// Initializes <c>ExamSet</c> with ID.
        /// </summary>
        /// <param name="id"><c>ExamSet</c> ID.</param>
        public ExamSet(Guid id)
        {
            Id = id;
        }
    }
}