using System;
using Theseus.Code.MVVM.Models.Maze.GridStructure;

namespace Theseus.Code.MVVM.Models.Maze.Generators
{
    public abstract class MazeGenerator
    {
        public Grid GenerateMaze(int rows, int cols)
        {
            Grid mazeGrid = new Grid(rows, cols);
            Random rnd = new Random();

            this.ApplyAlgorithm(mazeGrid, rnd);

            return mazeGrid;
        }

        public abstract void ApplyAlgorithm(Grid mazeGrid, Random rnd);
    }

}
