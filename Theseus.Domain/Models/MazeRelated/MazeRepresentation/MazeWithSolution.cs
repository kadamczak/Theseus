using Theseus.Domain.Models.MazeRelated.Enums;

namespace Theseus.Domain.Models.MazeRelated.MazeRepresentation
{
    public class MazeWithSolution
    {
        public Guid? Id { get; set; } = default;
        public Maze Grid { get; }

        public List<Cell> SolutionPath { get; set; } = new List<Cell>();
        public Direction StartDirection { get; set; }
        public Direction EndDirection { get; set; }

        //todo - constructor with only id?

        public MazeWithSolution(Maze grid, Guid? id = null)
        {
            Id = id;
            Grid = grid;
        }
    }
}