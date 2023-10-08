using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeStructureGenerators.Implementations;
using Theseus.Domain.Models.MazeRelated.MazeStructureGenerators;

namespace Theseus.Domain.Models.MazeRelated.MazeGenerators
{
    public class MazeStructureGeneratorFactory
    {
        public MazeStructureGeneratorBase Create(MazeStructureGenAlgorithm algorithm)
        {
            switch (algorithm)
            {
                case MazeStructureGenAlgorithm.Binary:
                    return new BinaryTreeMazeStructureGenerator();
                case MazeStructureGenAlgorithm.Sidewinder:
                    return new SidewinderMazeStructureGenerator();
                case MazeStructureGenAlgorithm.AldousBroder:
                    return new AldousBroderMazeStructureGenerator();
                case MazeStructureGenAlgorithm.HuntAndKill:
                    return new HuntAndKillMazeStructureGenerator();
                case MazeStructureGenAlgorithm.Kruskal:
                    return new KruskalMazeStructureGenerator();
                default:
                    throw new NotImplementedException("Asked to generate a maze with algorithm that has not been implemented yet.");
            }
        }
    }
}
