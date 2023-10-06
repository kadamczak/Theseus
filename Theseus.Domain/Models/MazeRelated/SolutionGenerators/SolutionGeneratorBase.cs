using Theseus.Domain.Models.MazeRelated.MazeStructure;

namespace Theseus.Domain.Models.MazeRelated.SolutionGenerators
{
    public abstract class SolutionGeneratorBase
    {
        public SolvableMaze CreateSolvableMaze(Maze maze)
        {
            SolvableMaze solvableMaze = new SolvableMaze(maze);

            ApplyAlgorithm(solvableMaze);

            return solvableMaze;
        }

        public abstract void ApplyAlgorithm(SolvableMaze maze);
    }
}