using Theseus.Domain.Extensions;
using Theseus.Domain.Models.MazeRelated.MazeStructure;

namespace Theseus.Domain.Models.MazeRelated.Generators.Implementations
{
    public class KruskalMazeGenerator : MazeGeneratorBase
    {
        class State
        {
            private Maze _grid;
            private List<(Cell, Cell)> _neighbourPairs = new();
            private Dictionary<Cell, int> _cellToSetIdentifier = new();
            private Dictionary<int, HashSet<Cell>> _setIdentifierToCells = new();

            public State(Maze maze)
            {
                _grid = maze;
                
                foreach(var (cell, index) in maze.WithIndex())
                {
                    int setIdentifier = _cellToSetIdentifier.Count();

                    _cellToSetIdentifier[cell] = setIdentifier;
                }
            }
        }

        public override void ApplyAlgorithm(Maze maze, Random rnd)
        {
            throw new NotImplementedException();
        }
    }
}
