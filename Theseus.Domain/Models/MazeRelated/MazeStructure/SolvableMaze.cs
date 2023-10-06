namespace Theseus.Domain.Models.MazeRelated.MazeStructure
{
    public class SolvableMaze : Maze
    {
        public List<Cell> SolutionPath { get; } = new List<Cell>();

        public SolvableMaze(int rows, int columns, Guid? id = null) : base(rows, columns, id) {}
        public SolvableMaze(Maze maze) : base(maze) { }
    }
}