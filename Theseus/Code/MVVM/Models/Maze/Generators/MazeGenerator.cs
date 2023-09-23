using System;
using Theseus.Code.MVVM.Models.Maze.GridStructure;

namespace Theseus.Code.MVVM.Models.Maze.Generators
{
    public abstract class MazeGenerator
    {
        public MazeGrid GenerateMaze(int rows, int cols)
        {
            MazeGrid mazeGrid = new MazeGrid(rows, cols);
            Random rnd = new Random();

            this.ApplyAlgorithm(mazeGrid, rnd);

            return mazeGrid;
        }

        public abstract void ApplyAlgorithm(MazeGrid mazeGrid, Random rnd);
    }

}
