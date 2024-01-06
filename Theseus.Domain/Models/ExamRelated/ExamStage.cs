namespace Theseus.Domain.Models.ExamRelated
{
    /// <summary>
    /// The <c>ExamStage</c> class represents the course of solving a singular <c>MazeWithSolution</c> by a <c>Patient</c> during an <c>Exam</c>.
    /// </summary>
    public class ExamStage
    {
        /// <summary>
        /// Gets or sets <c>ExamStage</c> ID.
        /// </summary>
        public Guid Id { get; set; } = default!;

        /// <summary>
        /// Gets or sets the <c>Exam</c> that this <c>ExamStage</c> is a part of.
        /// </summary>
        public Exam Exam { get; set; } = default!;

        /// <summary>
        /// Gets or sets the ordering of the <c>ExamStage</c> within <c>Exam</c>.
        /// </summary>
        /// <remarks>
        /// Numbering begins with 0.
        /// </remarks>
        public int Index { get; set; } = 0;

        /// <summary>
        /// Gets or sets the <c>ExamStep</c> list.
        /// </summary>
        /// <remarks>
        /// Each <c>ExamStep</c> represents a singular <c>Patient</c> input.
        /// </remarks>
        public List<ExamStep> Steps { get; set; } = new List<ExamStep>();

        /// <summary>
        /// Gets or sets whether <c>Patient</c> solved the <c>MazeWithSolution</c> or skipped it.
        /// </summary>
        public bool Completed { get; set; } = false;

        /// <summary>
        /// Gets or sets the total time spent on the stage.
        /// </summary>
        public float TotalTime { get; set; } = default!;

        /// <summary>
        /// Initializes <c>ExamStage</c> with no additional actions.
        /// </summary>
        public ExamStage() { }

        /// <summary>
        /// Initializes <c>ExamStage</c> with <see cref="Id"/>
        /// </summary>
        /// <param name="id"><c>ExamStage</c> ID.</param>
        public ExamStage(Guid id)
        {
            Id = id;
        }
    }
}