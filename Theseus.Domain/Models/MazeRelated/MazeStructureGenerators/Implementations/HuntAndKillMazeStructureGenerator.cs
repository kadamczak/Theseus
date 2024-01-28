﻿using Theseus.Domain.Extensions;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.Models.MazeRelated.MazeStructureGenerators.Implementations
{
    /// <summary>
    /// The <c>HuntAndKillMazeStructureGenerator</c> class applies the Hunt-and-Kill algorithm on <c>Maze</c>.
    /// </summary>
    public class HuntAndKillMazeStructureGenerator : MazeStructureGeneratorBase
    {
        public override void GenerateMazeStructureInGrid(Maze mazeGrid, int? rndSeed = null)
        {
            Random rnd = CreateRandom(rndSeed);
            Cell? currentCell = mazeGrid.GetRandomCell(rnd);

            while (currentCell is not null)
            {
                var unvisitedNeighbours = GetNeighbours(currentCell, predicate: neighbour => !HasBeenVisited(neighbour));

                if (unvisitedNeighbours.Any())
                {
                    Cell randomUnvisitedNeighbour = unvisitedNeighbours.GetRandomItem(rnd);
                    currentCell.LinkTo(randomUnvisitedNeighbour);
                    currentCell = randomUnvisitedNeighbour;
                }
                else
                {
                    currentCell = HuntForNewCurrentCell(mazeGrid, rnd);
                }
            }
        }

        private bool HasBeenVisited(Cell cell) => cell.GetLinkedCells().Any();

        private IEnumerable<Cell> GetNeighbours(Cell cell, Func<Cell, bool> predicate) => cell.GetNeighbours().Where(predicate);

        private Cell? HuntForNewCurrentCell(Maze mazeGrid, Random rnd)
        {
            foreach (var cell in mazeGrid)
            {
                if (!HasBeenVisited(cell))
                {
                    var visitedNeighbours = GetNeighbours(cell, predicate: HasBeenVisited);

                    if(visitedNeighbours.Any())
                    {
                        Cell randomVisitedNeighbour = visitedNeighbours.GetRandomItem(rnd);
                        cell.LinkTo(randomVisitedNeighbour);
                        return cell;
                    }
                }
            }

            return null;
        }
    }
}