using Theseus.Domain.Models.GroupRelated;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Groups;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.Commands.GroupCommands
{
    public class ShowGroupDetailsCommand : CommandBase
    {
        private readonly CommandViewModel<Group> _groupCommandViewModel;
        private readonly SelectedGroupDetailsStore _selectedGroupDetailsStore;
        private readonly NavigationService<GroupDetailsViewModel> _groupDetailsNavigationService;

        public ShowGroupDetailsCommand(CommandViewModel<Group> groupCommandViewModel,
                                       SelectedGroupDetailsStore selectedGroupDetailsStore,
                                       NavigationService<GroupDetailsViewModel> groupDetailsNavigationService)
        {
            _groupCommandViewModel = groupCommandViewModel;
            _selectedGroupDetailsStore = selectedGroupDetailsStore;
            _groupDetailsNavigationService = groupDetailsNavigationService;
        }

        public override void Execute(object? parameter)
        {
            Group group = _groupCommandViewModel.Model;
            _selectedGroupDetailsStore.SelectedGroup = group;
            _groupDetailsNavigationService.Navigate();
        }
    }
}