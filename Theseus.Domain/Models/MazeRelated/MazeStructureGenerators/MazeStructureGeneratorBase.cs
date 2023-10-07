using Theseus.Domain.Models.MazeRelated.Maze;

namespace Theseus.Domain.Models.MazeRelated.MazeStructureGenerators
{
    public abstract class MazeStructureGeneratorBase
    {
        public abstract void GenerateMazeStructureInGrid(MazeGrid maze);
    }
}