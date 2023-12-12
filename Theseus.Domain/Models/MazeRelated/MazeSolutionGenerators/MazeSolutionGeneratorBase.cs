﻿using Theseus.Domain.Extensions;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators.HelperClasses;

namespace Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators
{
    public abstract class MazeSolutionGeneratorBase
    {
        public DistanceGridFactory DistanceGridFactory { get; }
        public bool ShouldExcludeCellsCloseToRoot { get; }

        public MazeSolutionGeneratorBase(DistanceGridFactory distanceGridFactory, bool shouldExcludeCellsCloseToRoot)
        {
            this.DistanceGridFactory = distanceGridFactory;
            this.ShouldExcludeCellsCloseToRoot = shouldExcludeCellsCloseToRoot;
        }

        public abstract void GenerateSolutionInMaze(MazeWithSolution maze, int? rndSeed = null);
        protected Random CreateRandom(int? rndSeed = null) => (rndSeed is null) ? new Random() : new Random(rndSeed.Value);

        protected Direction ChooseRandomExitDirection(Cell cell, Random rnd) => GetAdjecentEmptySpaces(cell).GetRandomItem(rnd);
        private IEnumerable<Direction> GetAdjecentEmptySpaces(Cell cell) => cell.AdjecentCellSpaces.Where(c => c.Value == null).ToDictionary(x => x.Key, x => x.Value).Keys;
    }
}