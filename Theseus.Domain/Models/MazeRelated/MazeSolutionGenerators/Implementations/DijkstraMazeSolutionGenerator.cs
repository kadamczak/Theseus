using Theseus.Domain.Extensions;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators.HelperClasses;

namespace Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators.Implementations
{
    /// <summary>
    /// The <c>DijkstraMazeSolutionGenerator</c> class generates longest possible path solution in <c>MazeWithSolution</c>.
    /// </summary>
    public class DijkstraMazeSolutionGenerator : MazeSolutionGeneratorBase
    {
        public DijkstraMazeSolutionGenerator(DistanceGridFactory distanceGridFactory, bool shouldExcludeCellsCloseToRoot)
            : base(distanceGridFactory, shouldExcludeCellsCloseToRoot){}

        public override void GenerateSolutionInMaze(MazeWithSolution maze, int? rndSeed = null)
        {
            maze.SolutionPath.Clear();
            Random rnd = CreateRandom(rndSeed);

            List<Cell> borderCells = maze.Grid.GetBorderCells().ToList();
            Cell startCell = FindStartCell(maze, rnd, borderCells);
            var distanceGrid = DistanceGridFactory.CreateDistanceGrid(startCell);

            if (ShouldExcludeCellsCloseToRoot)
                borderCells = maze.Grid.ExcludeCellsCloseTo(startCell, borderCells).ToList();

            Cell endCell = distanceGrid.FindFarthestCells(borderCells).GetRandomItem(rnd);

            maze.SolutionPath = distanceGrid.FindPathTo(endCell);
             maze.StartDirection = ChooseRandomExitDirection(startCell, rnd);
            maze.EndDirection = ChooseRandomExitDirection(endCell, rnd);
        }

        private Cell FindStartCell(MazeWithSolution maze, Random rnd, List<Cell> borderCells)
        {
            Cell firstCell = maze.Grid.GetCell(0, 0)!;
            var distanceGrid = DistanceGridFactory.CreateDistanceGrid(rootCell: firstCell);
            return distanceGrid.FindFarthestCells(borderCells).GetRandomItem(rnd);
        }
    }
}