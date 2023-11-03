using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.Models.MazeRelated.MazeStructureGenerators
{
    public abstract class MazeStructureGeneratorBase
    {
        public abstract void GenerateMazeStructureInGrid(Maze mazeGrid, int? rndSeed = null);
        public Random CreateRandom(int? rndSeed = null) => (rndSeed is null) ? new Random() : new Random(rndSeed.Value);
    }
}