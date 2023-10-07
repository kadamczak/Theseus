using Theseus.Domain.Models.MazeRelated.Enums;

namespace Theseus.WPF.Code.ViewModels.Bindings
{
    public class AlgorithmViewModel
    {
        public string DisplayName { get; }
        public MazeStructureGenAlgorithm Algorithm { get; }

        public AlgorithmViewModel(string name, MazeStructureGenAlgorithm alg)
        {
            DisplayName = name;
            Algorithm = alg;
        }
    }
}
