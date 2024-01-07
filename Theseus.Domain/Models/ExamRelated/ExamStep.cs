using Theseus.Domain.Models.MazeRelated.Enums;

namespace Theseus.Domain.Models.ExamRelated
{
    /// <summary>
    /// The <c>ExamStep</c> class represents a <c>Patient</c>'s singular input during an <c>ExamStage</c>.
    /// </summary>
    public class ExamStep
    {
        /// <summary>
        /// Gets or sets <c>ExamStep</c> ID.
        /// </summary>
        public Guid Id { get; set; } = default!;

        /// <summary>
        /// Gets or sets the <c>ExamStage</c> this <c>ExamStep</c> belongs to.
        /// </summary>
        public ExamStage Stage { get; set; } = default!;

        /// <summary>
        /// Gets or sets the ordering of the <c>ExamStep</c> within <c>ExamStage</c>.
        /// </summary>
        /// <remarks>
        /// Numbering begins with 0.
        /// </remarks>
        public int Index { get; set; } = default!;

        /// <summary>
        /// Gets or sets the <c>Direction</c> of the <c>Patient</c>'s input.
        /// </summary>
        public Direction StepTaken { get; set; } = default!;

        /// <summary>
        /// Gets or sets the time that passed between the previous <c>ExamStep</c> (or start of <c>ExamStage</c> if <see cref="Index"/> is 0) and this <c>ExamStep</c>.
        /// </summary>
        /// <remarks>
        /// Time is measured in seconds.
        /// </remarks>
        public float TimeBeforeStep { get; set; } = default!;

        public bool Correct { get; set; } = default!;
        public bool HitWall { get; set; } = default!;

        /// <summary>
        /// Initializes <c>ExamStep</c> with no additional actions.
        /// </summary>
        public ExamStep() { }

        /// <summary>
        /// Initializes <c>ExamStep</c> with ID.
        /// </summary>
        /// <param name="id"><c>ExamStep</c> ID.</param>
        public ExamStep(Guid id)
        {
            Id = id;
        }
    }
}