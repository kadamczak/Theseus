using System;
using System.Collections.Generic;
using System.Linq;
using Theseus.Code.MVVM.Models.Maze.Enums;
using Theseus.Code.MVVM.Models.Maze.GridStructure;

namespace Theseus.Code.MVVM.Models.Maze.Generators
{
    public class BinaryTreeMazeGenerator : MazeGenerator
    {
        public BinaryTreeMazeGenerator() {}

        public override void ApplyAlgorithm(MazeGrid mazeGrid, Random rnd)
        {
            foreach (var cell in mazeGrid)
            {
                var cellNeighbours = cell.GetAdjecentCells(Direction.North, Direction.East);

                if (cellNeighbours.Any())
                    LinkToRandomAdjecentCell(cell, cellNeighbours, rnd);
            }
        }

        private void LinkToRandomAdjecentCell(Cell currentCell, IEnumerable<Cell> cellNeighbours, Random rnd)
        {
            int index = rnd.Next(0, cellNeighbours.Count());
            Cell randomCell = cellNeighbours.ElementAt(index);

            currentCell.LinkTo(randomCell);
        }

    }
}
