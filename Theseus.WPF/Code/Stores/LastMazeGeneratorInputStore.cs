using Theseus.Domain.Models.MazeRelated.Enums;

namespace Theseus.WPF.Code.Stores
{
    public class LastMazeGeneratorInputStore
    {
        public string Height { get; set; } = "15";
        public string Width { get; set; } = "30";
        public MazeStructureGenAlgorithm StructureAlgorithm { get; set; } = MazeStructureGenAlgorithm.AldousBroder;
        public MazeSolutionGenAlgorithm SolutionAlgorithm { get; set; } = MazeSolutionGenAlgorithm.Dijkstra;
        public bool ShouldExcludeCellsCloseToRoot { get; set; } = true;
    }
}