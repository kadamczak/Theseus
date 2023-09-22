using System;
using Theseus.Code.MVVM.Models.Maze.Enums;

namespace Theseus.Code.Commands
{
    class GenerateMazeCommandParameters
    {
        public MazeGenAlgorithm Algorithm { get; }
        public int Width { get; }
        public int Height { get; }

        public GenerateMazeCommandParameters(object? packedParameters)
        {
            if (packedParameters is null)
                throw new ArgumentNullException("Parameter object passed from UI cannot be null.");

            object[] parameters = packedParameters as object[];

            this.Algorithm = (MazeGenAlgorithm) parameters![0];
            this.Width = (int) parameters![1];
            this.Height = (int) parameters![2];
        }

    }
}
