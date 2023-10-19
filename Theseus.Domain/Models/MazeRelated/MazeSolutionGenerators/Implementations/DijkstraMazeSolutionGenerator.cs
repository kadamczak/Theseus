using Theseus.Domain.Extensions;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators.HelperClasses;

namespace Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators.Implementations
{
    public class DijkstraMazeSolutionGenerator : MazeSolutionGeneratorBase
    {
        public DijkstraMazeSolutionGenerator(DistanceGridFactory distanceGridFactory, bool shouldExcludeCellsCloseToRoot)
            : base(distanceGridFactory, shouldExcludeCellsCloseToRoot){}

        public override void GenerateSolutionInMaze(MazeWithSolution maze)
        {
            maze.SolutionPath.Clear();
            Random rnd = new Random();

            List<Cell> borderCells = maze.Grid.GetBorderCells().ToList();
            Cell rootCell = FindRootCell(maze, rnd, borderCells);
            var distanceGrid = DistanceGridFactory.CreateDistanceGrid(rootCell);

            if (ShouldExcludeCellsCloseToRoot)
                borderCells = maze.Grid.ExcludeCellsCloseTo(rootCell, borderCells).ToList();

            Cell endCell = distanceGrid.FindFarthestCells(borderCells).GetRandomItem(rnd);

            maze.SolutionPath = distanceGrid.FindPathTo(endCell);
            maze.StartDirection = ChooseRandomExitDirection(rootCell, rnd);
            maze.EndDirection = ChooseRandomExitDirection(endCell, rnd);
        }

        private Cell FindRootCell(MazeWithSolution maze, Random rnd, List<Cell> borderCells)
        {
            Cell firstCell = maze.Grid.GetCell(0, 0)!;
            var distanceGrid = DistanceGridFactory.CreateDistanceGrid(rootCell: firstCell);
            return distanceGrid.FindFarthestCells(borderCells).GetRandomItem(rnd);
        }
    }
}