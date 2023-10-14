using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators.HelperClasses;

namespace Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators.Implementations
{
    public class RandomBorderCellsMazeSolutionGenerator : MazeSolutionGeneratorBase
    {
        public RandomBorderCellsMazeSolutionGenerator(DistanceGridFactory distanceGridFactory) : base(distanceGridFactory) {}

        public override void GenerateSolutionInMaze(MazeWithSolution maze)
        {
            maze.SolutionPath.Clear();
            Random rnd = new Random();

            if(maze.Grid.RowAmount == 1 && maze.Grid.ColumnAmount == 1)
            {
                maze.SolutionPath.Add(maze.Grid.GetCell(0, 0)!);
                return;
            }

            (int, int) rootCellCoordinates = GetRandomPair(maze.Grid.RowAmount, maze.Grid.ColumnAmount, rnd);
            (int, int) endCellCoordinates;
            do
            {
                endCellCoordinates = GetRandomPair(maze.Grid.RowAmount, maze.Grid.ColumnAmount, rnd);
            }
            while (endCellCoordinates == rootCellCoordinates);

            Cell rootCell = maze.Grid.GetCell(rootCellCoordinates)!;
            Cell endCell = maze.Grid.GetCell(endCellCoordinates)!;

            DistanceGrid distanceGrid = DistanceGridFactory.CreateDistanceGrid(rootCell, maze.Grid.RowAmount, maze.Grid.ColumnAmount);

            maze.SolutionPath = distanceGrid.FindPathTo(endCell);
            maze.StartDirection = ChooseRandomExitDirection(rootCell, rnd);
            maze.EndDirection = ChooseRandomExitDirection(endCell, rnd);
        }

        private (int, int) GetRandomPair(int max1, int max2, Random rnd)
        {
            int first = rnd.Next(0, max1);
            int second = rnd.Next(0, max2);
            return (first, second);
        }
    }
}