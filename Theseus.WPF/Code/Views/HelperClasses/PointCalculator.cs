using System.Drawing;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.WPF.Code.Views.HelperClasses
{
    public class PointCalculator
    {
        public PointF CalculateCellCenter(Cell cell, float cellSize) => new PointF(x: cell.ColumnIndex * cellSize + cellSize / 2,
                                                                                   y: cell.RowIndex * cellSize + cellSize / 2);

        public PointF CalculatePointInDirection(Direction direction, PointF origin, float distance)
        {
            return direction switch
            {
                Direction.West => new PointF(x: origin.X - distance, y: origin.Y),
                Direction.North => new PointF(x: origin.X, y: origin.Y - distance),
                Direction.East => new PointF(x: origin.X + distance, y: origin.Y),
                Direction.South => new PointF(x: origin.X, y: origin.Y + distance),
            };
        }
    }
}
