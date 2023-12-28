using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeStructureGenerators.Implementations;
using Theseus.Domain.Models.MazeRelated.MazeStructureGenerators;

namespace Theseus.Domain.Models.MazeRelated.MazeGenerators
{
    public class MazeStructureGeneratorFactory
    {
        public MazeStructureGeneratorBase Create(MazeStructureGenAlgorithm algorithm)
        {
            return algorithm switch
            {
                MazeStructureGenAlgorithm.Binary => new BinaryTreeMazeStructureGenerator(),
                MazeStructureGenAlgorithm.Sidewinder => new SidewinderMazeStructureGenerator(),
                MazeStructureGenAlgorithm.AldousBroder => new AldousBroderMazeStructureGenerator(),
                MazeStructureGenAlgorithm.HuntAndKill => new HuntAndKillMazeStructureGenerator(),
                MazeStructureGenAlgorithm.Kruskal => new KruskalMazeStructureGenerator(),
                MazeStructureGenAlgorithm.TruePrim => new TruePrimMazeStructureGenerator(),
                _ => throw new NotImplementedException("Asked to generate a maze with algorithm that has not been implemented yet."),
            };
        }
    }
}
