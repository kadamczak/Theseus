using Theseus.Domain.Models.MazeRelated.Enums;

namespace Theseus.WPF.Code.ViewModels.Bindings
{
    public class SolutionAlgorithmViewModel : LabelValueViewModel<MazeSolutionGenAlgorithm>
    {
        public SolutionAlgorithmViewModel(string label, MazeSolutionGenAlgorithm value) : base(label, value)
        {
        }
    }
}