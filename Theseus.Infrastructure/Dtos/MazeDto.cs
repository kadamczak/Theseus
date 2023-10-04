using System.ComponentModel.DataAnnotations;
using Theseus.Domain.Models.MazeRelated.MazeStructure;

namespace Theseus.Infrastructure.Dtos
{
    public class MazeDto
    {
        [Key]
        public Guid? Id { get; set; } = default;
        public int Height { get; set; } = default!;
        public int Width { get; set; } = default!;
        public byte[] Data { get; set; } = default!;

        public MazeDto(Maze maze, byte[] cellsAsBytes)
        {
            Id = maze.Id;
            Height = maze.RowAmount;
            Width = maze.ColumnAmount;
            Data = cellsAsBytes;
        }

        public MazeDto(Guid? id, int height, int width, byte[] data)
        {
            Id = id;
            Height = height;
            Width = width;
            Data = data;
        }
    }
}
