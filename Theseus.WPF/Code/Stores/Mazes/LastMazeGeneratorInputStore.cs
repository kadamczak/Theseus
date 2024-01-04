using Theseus.Domain.Models.MazeRelated.Enums;

namespace Theseus.WPF.Code.Stores.Mazes
{
    public class LastMazeGeneratorInputStore
    {
        public string Height { get; set; } = "15";
        public string Width { get; set; } = "23";
        public MazeStructureGenAlgorithm StructureAlgorithm { get; set; } = MazeStructureGenAlgorithm.AldousBroder;
        public MazeSolutionGenAlgorithm SolutionAlgorithm { get; set; } = MazeSolutionGenAlgorithm.Dijkstra;
        public bool ShouldExcludeCellsCloseToRoot { get; set; } = true;
    }
}