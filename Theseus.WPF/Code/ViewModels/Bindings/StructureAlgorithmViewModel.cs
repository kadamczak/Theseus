using Theseus.Domain.Models.MazeRelated.Enums;

namespace Theseus.WPF.Code.ViewModels.Bindings
{
    public class StructureAlgorithmViewModel
    {
        public string DisplayName { get; }
        public MazeStructureGenAlgorithm Algorithm { get; }

        public StructureAlgorithmViewModel(string name, MazeStructureGenAlgorithm alg)
        {
            DisplayName = name;
            Algorithm = alg;
        }
    }
}
