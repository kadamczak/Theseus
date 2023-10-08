namespace Theseus.Domain.Models.MazeRelated.MazeRepresentation
{
    public class MazeWithSolution
    {
        public Guid? Id { get; set; } = default;
        public Maze Grid { get; }
        public List<Cell> SolutionPath { get; set; } = new List<Cell>();

        public MazeWithSolution(Maze grid, Guid? id = null)
        {
            Id = id;
            Grid = grid;
        }
    }
}