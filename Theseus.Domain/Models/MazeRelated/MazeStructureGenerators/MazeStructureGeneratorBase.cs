using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.Models.MazeRelated.MazeStructureGenerators
{
    /// <summary>
    /// The <c>MazeStructureGeneratorBase</c> is an abstract base class for classes that manipulate <c>Cell</c> links inside <c>Maze</c> objects,
    /// creating a structure of walls and corridors.
    /// </summary>
    public abstract class MazeStructureGeneratorBase
    {
        public abstract void GenerateMazeStructureInGrid(Maze mazeGrid, int? rndSeed = null);
        protected Random CreateRandom(int? rndSeed = null) => (rndSeed is null) ? new Random() : new Random(rndSeed.Value);
    }
}