using System;
using Theseus.Code.MVVM.Models.Maze.Enums;

namespace Theseus.Code.MVVM.Models.Maze.Generators
{
    public class MazeGeneratorFactory
    {
        public static MazeGenerator Create(MazeGenAlgorithm algorithm)
        {
            switch(algorithm)
            {
                case MazeGenAlgorithm.Binary:
                    return new BinaryTreeMazeGenerator();
                case MazeGenAlgorithm.Sidewinder:
                    return new SidewinderMazeGenerator();
                default:
                    throw new NotImplementedException(":p");
            }
        }


    }
}
