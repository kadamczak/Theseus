using Theseus.Domain.Models.MazeRelated.Enums;

namespace Theseus.WPF.Code.ViewModels.Bindings
{
    public class AlgorithmViewModel
    {
        public string DisplayName { get; }
        public MazeGenAlgorithm Algorithm { get; }

        public AlgorithmViewModel(string name, MazeGenAlgorithm alg)
        {
            DisplayName = name;
            Algorithm = alg;
        }
    }
}
