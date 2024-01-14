using Theseus.Domain.Models.MazeRelated.Enums;

namespace Theseus.Domain.Extensions
{
    /// <summary>
    /// Class containing extension methods for the Direction enum.
    /// </summary>
    public static class DirectionExtension
    {
        /// <summary>
        /// Returns opposite <c>Direction</c> to <paramref name="self"/> - for example North turns into South.
        /// </summary>
        /// <param name="self">Original direction.</param>
        /// <returns>Direction opposite to the original direction.</returns>
        /// <exception cref="NullReferenceException">If self is null.</exception>
        public static Direction Reverse(this Direction self)
        {
            return self switch
            {
                Direction.North => Direction.South,
                Direction.South => Direction.North,
                Direction.East => Direction.West,
                Direction.West => Direction.East,
                _ => throw new NullReferenceException("Can't reverse null Direction.")
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
                Direction.North => originalRow - 1,
                Direction.South => originalRow + 1,
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
                Direction.West => originalColumn - 1,
                Direction.East => originalColumn + 1,
                _ => originalColumn
            };
        }
    }
}