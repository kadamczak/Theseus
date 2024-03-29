﻿using Theseus.Domain.Extensions;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators.HelperClasses;

namespace Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators.Implementations
{
    /// <summary>
    /// The <c>RandomBorderCellsMazeSolutionGenerator</c> class generates random solution in <c>MazeWithSolution</c>.
    /// </summary>
    public class RandomBorderCellsMazeSolutionGenerator : MazeSolutionGeneratorBase
    {
        public RandomBorderCellsMazeSolutionGenerator(DistanceGridFactory distanceGridFactory, bool shouldExcludeCellsCloseToRoot)
            : base(distanceGridFactory, shouldExcludeCellsCloseToRoot) {}

        public override void GenerateSolutionInMaze(MazeWithSolution maze, int? rndSeed = null)
        {
            maze.SolutionPath.Clear();
            Random rnd = CreateRandom(rndSeed);

            List<Cell> borderCells = maze.Grid.GetBorderCells().ToList();
            Cell rootCell = borderCells.GetRandomItem(rnd);
            borderCells.Remove(rootCell);

            if (ShouldExcludeCellsCloseToRoot)
                borderCells = maze.Grid.ExcludeCellsCloseTo(rootCell, borderCells).ToList();

            Cell endCell = borderCells.GetRandomItem(rnd);
            DistanceGrid distanceGrid = DistanceGridFactory.CreateDistanceGrid(rootCell);

            maze.SolutionPath = distanceGrid.FindPathTo(endCell);
            maze.StartDirection = ChooseRandomExitDirection(rootCell, rnd);
            maze.EndDirection = ChooseRandomExitDirection(endCell, rnd);
        }
    }
}