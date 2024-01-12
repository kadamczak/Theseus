using Theseus.Domain.Models.MazeRelated.Enums;

namespace Theseus.Domain.Extensions
{
    public static class DirectionExtension
    {
        public static Direction Reverse(this Direction self)
        {
            return self switch
            {
                Direction.North => Direction.South,
                Direction.South => Direction.North,
                Direction.East => Direction.West,
                Direction.West => Direction.East,
                _ => throw new ArgumentNullException("Can't reverse null Direction.")
            };
        }

        /// <summary>
        /// Calculates neighbour row index in the specified <c>Direction</c> from the original row.
        /// </summary>
        /// <param name="originalRow">Original row index.</param>
        /// <param name="directionOfNeighbour">Relative neighbour <c>Direction</c>.</param>
        /// <returns>Neighbour row index in the specified <c>Direction</c> from the original row.</returns>
        public static int CalculateRow(this Direction directionOfNeighbour, int originalRow)
        {
            return directionOfNeighbour switch
            {
                Direction.North => originalRow--,
                Direction.South => originalRow++,
                _ => originalRow
            };
        }

        /// <summary>
        /// Calculates neighbour column index in the specified <c>Direction</c> from the original column.
        /// </summary>
        /// <param name="originalColumn">Original column index.</param>
        /// <param name="directionOfNeighbour">Relative neighbour <c>Direction</c>.</param>
        /// <returns>>Neighbour column index in the specified <c>Direction</c> from the original column.</returns>
        public static int CalculateColumn(this Direction directionOfNeighbour, int originalColumn)
        {
            return directionOfNeighbour switch
            {
                Direction.West => originalColumn--,
                Direction.East => originalColumn++,
                _ => originalColumn
            };
        }
    }
}