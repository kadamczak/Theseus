namespace Theseus.Code.MVVM.Models.Maze.Entity
{
    public class MazeEntity
    {
        public int Id { get; set; } = default!;
        public int Height { get; set; } = default!;
        public int Width { get; set; } = default!;
        public byte[] Data { get; set; } = default!;

        public MazeEntity(int height, int width, byte[] data)
        {        
            Height = height;
            Width = width;
            Data = data;
        }
    }
}
