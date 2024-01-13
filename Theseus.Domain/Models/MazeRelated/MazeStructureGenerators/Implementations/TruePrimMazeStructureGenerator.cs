using Theseus.Domain.Extensions;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.Models.MazeRelated.MazeStructureGenerators.Implementations
{
    public class TruePrimMazeStructureGenerator : MazeStructureGeneratorBase
    {
        public override void GenerateMazeStructureInGrid(Maze mazeGrid, int? rndSeed = null)
        {
            Random rnd = CreateRandom(rndSeed);
            var activeCells = new List<Cell>() { mazeGrid.GetRandomCell(rnd) };
            var cellCosts = AssignRandomCostsToCells(rnd, mazeGrid, 0, 100);

            while(activeCells.Any())
            {
                Cell currentCell = GetMinimumCostCell(activeCells, cellCosts, rnd);
                var notLinkedNeighbours = currentCell.GetAdjecentCells().Where(n => !n.GetLinkedCells().Any());

                if(notLinkedNeighbours.Any())
                {
                    Cell neighbour = GetMinimumCostCell(notLinkedNeighbours, cellCosts, rnd);
                    currentCell.LinkTo(neighbour);
                    activeCells.Add(neighbour);
                }
                else
                {
                    activeCells.Remove(currentCell);
                }
            }
        }

        private Dictionary<Cell, int> AssignRandomCostsToCells(Random rnd, Maze maze, int minValue, int maxValue)
        {
            var cellCosts = new Dictionary<Cell, int>();
            foreach(var cell in maze)
            {
                cellCosts.Add(cell, rnd.Next(minValue, maxValue));
            }
            return cellCosts;
        }

        private Cell GetMinimumCostCell(IEnumerable<Cell> cells, Dictionary<Cell, int> cellCosts, Random rnd)
        {
            var costs = cellCosts.Where(c => cells.Contains(c.Key));
            int minCost = costs.Min(c => c.Value);
            return costs.Where(c => c.Value == minCost).GetRandomItem(rnd).Key;
        }
    }
}