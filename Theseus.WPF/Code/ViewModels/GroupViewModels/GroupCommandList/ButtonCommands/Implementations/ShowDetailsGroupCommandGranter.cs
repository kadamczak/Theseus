using System.Collections.ObjectModel;
using Theseus.Domain.Models.GroupRelated;
using Theseus.WPF.Code.Commands.GroupCommands;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels
{
    public class ShowDetailsGroupCommandGranter : CommandGranter<Group>
    {
        private readonly SelectedModelDetailsStore<Group> _selectedModelDetailsStore;
        private readonly NavigationService<GroupDetailsViewModel> _groupDetailsNavigationService;

        public ShowDetailsGroupCommandGranter(SelectedModelDetailsStore<Group> selectedModelDetailsStore,
                                              NavigationService<GroupDetailsViewModel> groupDetailsNavigationService)
        {
            _selectedModelDetailsStore = selectedModelDetailsStore;
            _groupDetailsNavigationService = groupDetailsNavigationService;
        }

        public override ButtonViewModel GrantCommand(ObservableCollection<CommandViewModel<Group>> collection,
                                                     CommandViewModel<Group> commandViewModel)
        {
            return new ButtonViewModel(true,
                                       "Details".Resource(),
                                       new ShowGroupDetailsCommand(commandViewModel, _selectedModelDetailsStore, _groupDetailsNavigationService));
        }
    }
}