using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.Models.MazeRelated.MazeRepresentation
{
    /// <summary>
    /// The <c>MazeWithSolution</c> class add a solution to the <c>Maze</c> class.
    /// </summary>
    public class MazeWithSolution
    {
        /// <summary>
        /// Gets or sets the <c>MazeWithSolution</c> ID.
        /// </summary>
        public Guid? Id { get; set; } = default;

        /// <summary>
        /// Gets the underlying <c>Maze</c>.
        /// </summary>
        public Maze Grid { get; }

        /// <summary>
        /// Gets or sets an ordered list of <c>Cell</c>s that represent the solution of the <c>Maze</c>.
        /// </summary>
        public List<Cell> SolutionPath { get; set; } = new List<Cell>();

        /// <summary>
        /// Gets or sets the <c>Direction</c> of maze entry relative to the start <c>Cell</c> in <see cref="SolutionPath"/>.
        /// </summary>
        public Direction StartDirection { get; set; }

        /// <summary>
        /// Gets or sets the <c>Direction</c> of maze exit relative to the end <c>Cell</c> in <see cref="SolutionPath"/>.
        /// </summary>
        public Direction EndDirection { get; set; }

        /// <summary>
        /// Gets or sets the owner of the <c>MazeWithSolution</c>.
        /// </summary>
        public StaffMember Owner { get; set; }

        /// <summary>
        /// Initializes <c>MazeWithSolution</c> with <c>Maze</c> and potential ID.
        /// </summary>
        /// <param name="grid"><c>Maze</c> structure.</param>
        /// <param name="id">Nullable <c>MazeWithSolution</c> ID.</param>
        public MazeWithSolution(Maze grid, Guid? id = null)
        {
            Id = id;
            Grid = grid;
        }
    }
}