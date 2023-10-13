﻿using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators.HelperClasses;
using Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators.Implementations;

namespace Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators
{
    public class MazeSolutionGeneratorFactory
    {
        private readonly DistanceGridFactory _distanceGridFactory;
        public MazeSolutionGeneratorFactory(DistanceGridFactory distanceGridFactory)
        {
            this._distanceGridFactory = distanceGridFactory;
        }

        public MazeSolutionGeneratorBase Create(MazeSolutionGenAlgorithm algorithm)
        {
            switch (algorithm)
            {
                case MazeSolutionGenAlgorithm.Dijkstra:
                    return new DijkstraMazeSolutionGenerator(_distanceGridFactory);
                case MazeSolutionGenAlgorithm.RandomBorderCells:
                    return new RandomBorderCellsMazeSolutionGenerator(_distanceGridFactory);
                default:
                    throw new NotImplementedException("Asked to generate a maze solution with algorithm that has not been implemented yet.");
            }
        }
    }
}
