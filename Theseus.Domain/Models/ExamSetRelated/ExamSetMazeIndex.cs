using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.Models.ExamSetRelated
{
    /// <summary>
    /// The <c>ExamSetMazeIndex</c> class represents an <c>MazeWithSolution</c> and its order within an <c>ExamSet</c>.
    /// </summary>
    public class ExamSetMazeIndex
    {
        /// <summary>
        /// Gets or sets <c>ExamSetMazeIndex</c> ID.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the <c>ExamSet</c> this <c>ExamSetMazeIndex</c> is a part of.
        /// </summary>
        public ExamSet ExamSet { get; set; }

        /// <summary>
        /// Gets or sets the <c>MazeWithSolution</c> corresponding to the <see cref="Index"/> in this <c>ExamSet</c>.
        /// </summary>
        public MazeWithSolution MazeWithSolution { get; set; }

        /// <summary>
        /// Gets or sets the ordering of the <c>ExamSetMazeIndex</c> within <c>Exam</c>.
        /// </summary>
        /// <remarks>
        /// Numbering begins with 0.
        /// </remarks>
        public int Index { get; set; }

        /// <summary>
        /// Initializes <c>ExamSetMazeIndex</c> with no additional actions.
        /// </summary>
        public ExamSetMazeIndex() { }

        /// <summary>
        /// Initializes <c>ExamSetMazeIndex</c> with an <see cref="Id"/>, <c>ExamSet</c>, <c>MazeWithSolution</c> and <see cref="Index"/>.
        /// </summary>
        /// <param name="id"><c>ExamSetMazeIndex</c> ID.</param>
        /// <param name="examSet"><c>ExamSet</c> that <c>ExamSetMazeIndex</c> is a part of.</param>
        /// <param name="mazeWithSolution"><c>MazeWithSolution</c> corresponding to the <see cref="Index"/> in this <c>ExamSet</c>.</param>
        /// <param name="index">The ordering of the <c>ExamSetMazeIndex</c> within <c>Exam</c>.</param>
        public ExamSetMazeIndex(Guid id, ExamSet examSet, MazeWithSolution mazeWithSolution, int index)
        {
            Id = id;
            ExamSet = examSet;
            MazeWithSolution = mazeWithSolution;
            Index = index;
        }
    }
}