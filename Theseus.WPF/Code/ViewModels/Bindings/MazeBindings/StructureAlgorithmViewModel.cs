using Theseus.Domain.Models.MazeRelated.Enums;

namespace Theseus.WPF.Code.ViewModels.Bindings
{
    public class StructureAlgorithmViewModel : LabelValueViewModel<MazeStructureGenAlgorithm>
    {
        public StructureAlgorithmViewModel(string label, MazeStructureGenAlgorithm value) : base(label, value)
        {
        }
    }
}
