using System;
using Theseus.Domain.Extensions;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeStructure;

namespace Theseus.Domain.Models.MazeRelated.Generators.Implementations
{
    public class KruskalMazeGenerator : MazeGeneratorBase
    {
        public override void ApplyAlgorithm(Maze maze, Random rnd)
        {
            AlgorithmState algorithmState = new AlgorithmState(maze);

            var shuffledNeighbours = algorithmState.NeighbourPairs.FisherYatesShuffle();

            while (shuffledNeighbours.Any())
            {
                var (firstCell, secondCell) = shuffledNeighbours.Last();
                shuffledNeighbours.Remove(shuffledNeighbours.Last());

                if (algorithmState.AreInDifferentSets(firstCell, secondCell))
                {
                    algorithmState.MergeSets(firstCell, secondCell);
                }
            }
        }

        class AlgorithmState
        {
            public List<(Cell, Cell)> NeighbourPairs { get; } = new();

            private Dictionary<Cell, int> _cellToSetIdentifier = new();
            private Dictionary<int, HashSet<Cell>> _setIdentifierToCells = new();

            public AlgorithmState(Maze maze)
            {        
                foreach(var (cell, setIdentifier) in maze.WithIndex())
                {
                    _cellToSetIdentifier.Add(cell, setIdentifier);

                    _setIdentifierToCells.Add(setIdentifier, new HashSet<Cell>());
                    _setIdentifierToCells[setIdentifier].Add(cell);

                    var neighbours = cell.GetAdjecentCells(Direction.South, Direction.East);
                    
                    foreach (var neighbour in neighbours)
                    {
                        NeighbourPairs.Add((cell, neighbour));
                    }
                }
            }

            public bool AreInDifferentSets(Cell firstCell, Cell secondCell) => _cellToSetIdentifier[firstCell] != _cellToSetIdentifier[secondCell];

            public void MergeSets(Cell firstCell, Cell secondCell)
            {
                firstCell.LinkTo(secondCell);

                int remainingSet = _cellToSetIdentifier[firstCell];
                int absorbedSet = _cellToSetIdentifier[secondCell];

                foreach(var cell in _setIdentifierToCells[absorbedSet])
                {
                    AbsorbCellToSet(cell, remainingSet);
                }

                _setIdentifierToCells.Remove(absorbedSet);
            }

            private void AbsorbCellToSet(Cell cell, int setIdentifier)
            {
                _setIdentifierToCells[setIdentifier].Add(cell);
                _cellToSetIdentifier[cell] = setIdentifier;
            }
        }
    }
}
