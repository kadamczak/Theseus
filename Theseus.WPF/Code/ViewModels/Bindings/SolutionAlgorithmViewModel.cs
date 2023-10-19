using Theseus.Domain.Models.MazeRelated.Enums;

namespace Theseus.WPF.Code.ViewModels.Bindings
{
    public class SolutionAlgorithmViewModel
    {
        public string DisplayName { get; }
        public MazeSolutionGenAlgorithm Algorithm { get; }

        public SolutionAlgorithmViewModel(string name, MazeSolutionGenAlgorithm alg)
        {
            DisplayName = name;
            Algorithm = alg;
        }
    }
}