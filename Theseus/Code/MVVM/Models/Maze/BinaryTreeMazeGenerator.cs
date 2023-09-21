using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theseus.Code.MVVM.Models.Maze
{
    public class BinaryTreeMazeGenerator
    {
        public BinaryTreeMazeGenerator()
        {
        }

        public void GenerateMaze(Grid mazeGrid)
        {
            Random rnd = new Random();

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

            currentCell.LinkToAnotherCell(randomCell);
        }

    }
}
