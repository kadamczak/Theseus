using Theseus.Domain.Models.GroupRelated;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Groups;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.ViewModels.Bindings.GroupBindings;

namespace Theseus.WPF.Code.Commands.GroupCommands
{
    public class ShowGroupDetailsCommand : CommandBase
    {
        private readonly GroupCommandViewModel _groupCommandViewModel;
        private readonly SelectedGroupDetailsStore _selectedGroupDetailsStore;
        private readonly NavigationService<GroupDetailsViewModel> _groupDetailsNavigationService;

        public ShowGroupDetailsCommand(GroupCommandViewModel groupCommandViewModel,
                                       SelectedGroupDetailsStore selectedGroupDetailsStore,
                                       NavigationService<GroupDetailsViewModel> groupDetailsNavigationService)
        {
            _groupCommandViewModel = groupCommandViewModel;
            _selectedGroupDetailsStore = selectedGroupDetailsStore;
            _groupDetailsNavigationService = groupDetailsNavigationService;
        }

        public override void Execute(object? parameter)
        {
            Group group = _groupCommandViewModel.Group;
            _selectedGroupDetailsStore.SelectedGroup = group;
            _groupDetailsNavigationService.Navigate();
        }
    }
}