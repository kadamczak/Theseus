using Theseus.Domain.Models.MazeRelated.Enums;

namespace Theseus.WPF.Code.Stores
{
    public class LastMazeGeneratorInputStore
    {
        public MazeGenAlgorithm Algorithm { get; set; } = MazeGenAlgorithm.Binary;
        public string Height { get; set; } = "15";
        public string Width { get; set; } = "30";
    }
}