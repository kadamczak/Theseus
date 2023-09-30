using Theseus.Domain.Models.MazeRelated.MazeStructure;

namespace Theseus.Domain.Models.MazeRelated.Generators
{
    public abstract class MazeGeneratorBase
    {
        public Maze GenerateMaze(int rows, int cols)
        {
            Maze maze = new Maze(rows, cols);
            Random rnd = new Random();

            this.ApplyAlgorithm(maze, rnd);

            return maze;
        }

        public abstract void ApplyAlgorithm(Maze maze, Random rnd);
    }
}
