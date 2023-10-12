using Theseus.Domain.Extensions;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators.HelperClasses;

namespace Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators.Implementations
{
    public class DijkstraMazeSolutionGenerator : MazeSolutionGeneratorBase
    {
        public DijkstraMazeSolutionGenerator(DistanceGridFactory distanceGridFactory) : base(distanceGridFactory){}

        public override void GenerateSolutionInMaze(MazeWithSolution maze)
        {
            maze.SolutionPath.Clear();
            Random rnd = new Random();

            Cell rootCell = FindRootCell(maze, rnd);

            var distanceGridFromRoot = DistanceGridFactory.CreateDistanceGrid(rootCell);
            Cell endCell = distanceGridFromRoot.FindFarthestCells().GetRandomItem(rnd);

            maze.SolutionPath = distanceGridFromRoot.FindPathTo(endCell);
        }

        private Cell FindRootCell(MazeWithSolution maze, Random rnd)
        {
            Cell firstCell = maze.Grid.GetCell(0, 0)!;
            var distanceGrid = DistanceGridFactory.CreateDistanceGrid(rootCell: firstCell);
            return distanceGrid.FindFarthestCells().GetRandomItem(rnd);
        }
    }
}