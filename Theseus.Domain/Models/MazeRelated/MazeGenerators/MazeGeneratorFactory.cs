using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeGenerators.Implementations;

namespace Theseus.Domain.Models.MazeRelated.MazeGenerators
{
    public class MazeGeneratorFactory
    {
        public static MazeGeneratorBase Create(MazeGenAlgorithm algorithm)
        {
            switch (algorithm)
            {
                case MazeGenAlgorithm.Binary:
                    return new BinaryTreeMazeGenerator();
                case MazeGenAlgorithm.Sidewinder:
                    return new SidewinderMazeGenerator();
                case MazeGenAlgorithm.AldousBroder:
                    return new AldousBroderMazeGenerator();
                case MazeGenAlgorithm.HuntAndKill:
                    return new HuntAndKillMazeGenerator();
                case MazeGenAlgorithm.Kruskal:
                    return new KruskalMazeGenerator();
                default:
                    throw new NotImplementedException("Asked to generate a maze with algorithm that has not been implemented yet.");
            }
        }
    }
}
