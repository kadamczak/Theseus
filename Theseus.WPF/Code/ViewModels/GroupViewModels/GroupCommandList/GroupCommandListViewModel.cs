using Theseus.Domain.Models.GroupRelated;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;
using Theseus.WPF.Code.ViewModels.GroupViewModels.GroupCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.GroupViewModels.GroupCommandList.Info;

namespace Theseus.WPF.Code.ViewModels
{
    public class GroupCommandListViewModel : CommandListViewModel<Group, GroupButtonCommand, GroupInfo>
    {
        public GroupCommandListViewModel(SelectedModelListStore<Group> selectedModelListStore,
                                         CommandGranterFactory<Group, GroupButtonCommand> commandGranterFactory,
                                         InfoGranterFactory<Group, GroupInfo> infoGranterFactory,
                                         GroupButtonCommand command1,
                                         GroupButtonCommand command2,
                                         GroupInfo info) : base(selectedModelListStore, commandGranterFactory, infoGranterFactory, command1, command2, info)
        {
        }
    }
}