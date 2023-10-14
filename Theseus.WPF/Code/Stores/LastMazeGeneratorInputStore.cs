using Theseus.Domain.Models.MazeRelated.Enums;

namespace Theseus.WPF.Code.Stores
{
    public class LastMazeGeneratorInputStore
    {
        public MazeStructureGenAlgorithm StructureAlgorithm { get; set; } = MazeStructureGenAlgorithm.Binary;
        public string Height { get; set; } = "15";
        public string Width { get; set; } = "30";
    }
}