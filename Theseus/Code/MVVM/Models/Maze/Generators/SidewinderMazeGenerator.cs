using System;
using System.Collections.Generic;
using Theseus.Code.MVVM.Models.Maze.Enums;
using Theseus.Code.MVVM.Models.Maze.GridStructure;

namespace Theseus.Code.MVVM.Models.Maze.Generators
{
    public class SidewinderMazeGenerator : MazeGenerator
    {
        public SidewinderMazeGenerator() {}

        public override void ApplyAlgorithm(MazeGrid mazeGrid, Random rnd)
        {
            foreach (var row in mazeGrid.IterateRows())
            {
                List<Cell> cellRun = new List<Cell>();

                foreach (var cell in row)
                {
                    cellRun.Add(cell);

                    if (CheckIfShouldEndRun(cell, rnd)) //Links North
                    {
                        EndCellRun(cellRun, rnd);
                    }
                    else //Links East
                    {
                        LinkCellEast(cell);
                    }
                }
            }
        }


        private bool CheckIfShouldEndRun(Cell currentCell, Random rnd)
        {
            bool hasEastNeighbour = currentCell.AdjecentCellSpaces[Direction.East] is not null;
            bool hasNorthNeighbour = currentCell.AdjecentCellSpaces[Direction.North] is not null;

            if (!hasEastNeighbour)
            {
                return true;
            }
            else if (hasNorthNeighbour && ShouldEraseNorthBorder(rnd))
            {
                return true;
            }

            return false;
        }

        private bool ShouldEraseNorthBorder(Random rnd)
        {
            return rnd.Next(0, 2) == 1;
        }

        private void EndCellRun(List<Cell> cellRun, Random rnd)
        {
            int index = rnd.Next(0, cellRun.Count);
            Cell randomCell = cellRun[index];
            Cell? northCell = randomCell.AdjecentCellSpaces[Direction.North];

            if (northCell is not null)
                randomCell.LinkToAnotherCell(northCell);

            cellRun.Clear();
        }

        private void LinkCellEast(Cell currentCell)
        {
            Cell? eastCell = currentCell.AdjecentCellSpaces[Direction.East];

            if (eastCell is not null)
                currentCell.LinkToAnotherCell(eastCell);
        }

    }
}
