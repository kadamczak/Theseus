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
    }
}