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

            var distanceGridFromRoot = DistanceGridFactory.CreateDistanceGrid(rootCell, maze.Grid.RowAmount, maze.Grid.ColumnAmount);
            Cell endCell = distanceGridFromRoot.FindFarthestBorderCells(true).GetRandomItem(rnd);

            maze.SolutionPath = distanceGridFromRoot.FindPathTo(endCell);
            maze.StartDirection = ChooseRandomExitDirection(rootCell, rnd);
            maze.EndDirection = ChooseRandomExitDirection(endCell, rnd);
        }

        private Cell FindRootCell(MazeWithSolution maze, Random rnd)
        {
            Cell firstCell = maze.Grid.GetCell(0, 0)!;
            var distanceGrid = DistanceGridFactory.CreateDistanceGrid(rootCell: firstCell, maze.Grid.RowAmount, maze.Grid.ColumnAmount);
            return distanceGrid.FindFarthestBorderCells(true).GetRandomItem(rnd);
        }
    }
}