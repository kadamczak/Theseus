using System.ComponentModel.DataAnnotations;

namespace Theseus.Infrastructure.Dtos
{
    public class MazeDto
    {
        [Key]
        public Guid Id { get; set; } = default!;
        public int Height { get; set; } = default!;
        public int Width { get; set; } = default!;
        public byte[] Data { get; set; } = default!;

        public MazeDto(int height, int width, byte[] data)
        {
            Height = height;
            Width = width;
            Data = data;
        }
    }
}
