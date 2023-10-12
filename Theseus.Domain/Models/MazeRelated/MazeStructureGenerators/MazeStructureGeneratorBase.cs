using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.Models.MazeRelated.MazeStructureGenerators
{
    public abstract class MazeStructureGeneratorBase
    {
        public abstract void GenerateMazeStructureInGrid(Maze mazeGrid);
    }
}