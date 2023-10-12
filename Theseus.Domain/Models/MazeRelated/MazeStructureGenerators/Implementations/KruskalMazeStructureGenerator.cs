using Theseus.Domain.Extensions;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.Models.MazeRelated.MazeStructureGenerators.Implementations
{
    public class KruskalMazeStructureGenerator : MazeStructureGeneratorBase
    {
        public override void GenerateMazeStructureInGrid(Maze mazeGrid)
        {
            AlgorithmState algorithmState = new AlgorithmState(mazeGrid);
            var shuffledNeighbourPairs = algorithmState.NeighbourPairs.FisherYatesShuffle();

            while (shuffledNeighbourPairs.Any())
            {
                var (firstCell, secondCell) = PopLastPair(shuffledNeighbourPairs);

                if (algorithmState.AreInDifferentSets(firstCell, secondCell))
                {
                    algorithmState.MergeSets(firstCell, secondCell);
                }
            }
        }

        private (Cell, Cell) PopLastPair(IList<(Cell, Cell)> cellPairs)
        {
            var lastPair = cellPairs.Last();
            cellPairs.Remove(lastPair);
            return lastPair;
        }

        class AlgorithmState
        {
            public List<(Cell, Cell)> NeighbourPairs { get; } = new();

            private Dictionary<Cell, int> _cellToSetIdentifier = new();
            private Dictionary<int, HashSet<Cell>> _setIdentifierToCells = new();

            public AlgorithmState(Maze mazeGrid)
            {        
                foreach(var (cell, setIdentifier) in mazeGrid.WithIndex())
                {
                    AddCellToNewSet(cell, setIdentifier);
                    SaveNeighbourPairsOfCell(cell);
                }
            }

            private void AddCellToNewSet(Cell currentCell, int setIdentifier)
            {
                _cellToSetIdentifier.Add(currentCell, setIdentifier);
                _setIdentifierToCells.Add(setIdentifier, new HashSet<Cell>() { currentCell });
            }

            private void SaveNeighbourPairsOfCell(Cell currentCell)
            {
                var neighbours = currentCell.GetAdjecentCells(Direction.South, Direction.East);

                foreach (var neighbour in neighbours)
                {
                    NeighbourPairs.Add((currentCell, neighbour));
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
