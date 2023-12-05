using System.Collections.ObjectModel;
using Theseus.Domain.Models.ExamRelated;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.DataViewModels.ExamCommandList.ButtonCommands.Implementations
{
    public class EmptyExamCommandGranter : CommandGranter<Exam>
    {
        public override ButtonViewModel GrantCommand(ObservableCollection<CommandViewModel<Exam>> collection, CommandViewModel<Exam> commandViewModel)
        {
            return new ButtonViewModel(show: false);
        }
    }
}