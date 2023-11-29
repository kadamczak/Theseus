using System.Collections.ObjectModel;
using Theseus.Domain.Models.GroupRelated;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels
{
    public class EmptyGroupCommandGranter : CommandGranter<Group>
    {
        public override ButtonViewModel GrantCommand(ObservableCollection<CommandViewModel<Group>> collection,
                                                     CommandViewModel<Group> commandViewModel)
        {
            return new ButtonViewModel(false);
        }
    }
}