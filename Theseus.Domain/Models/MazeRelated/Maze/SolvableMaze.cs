namespace Theseus.Domain.Models.MazeRelated.Maze
{
    public class SolvableMaze : MazeGrid
    {
        public List<Cell> SolutionPath { get; } = new List<Cell>();

        public SolvableMaze(int rows, int columns, Guid? id = null) : base(rows, columns, id) {}
    }
}