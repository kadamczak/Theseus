using System.Collections.ObjectModel;
using Theseus.WPF.Code.Commands.MazeCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels
{
    public class ShowDetailsMazeCommandGranter : CommandGranter<MazeWithSolutionCanvasViewModel>
    {
        private readonly SelectedModelDetailsStore<MazeWithSolutionCanvasViewModel> _mazeDetailsStore;
        private readonly NavigationService<MazeDetailsViewModel> _mazeDetailsNavigationService;

        public ShowDetailsMazeCommandGranter(SelectedModelDetailsStore<MazeWithSolutionCanvasViewModel> mazeDetailsStore,
                                             NavigationService<MazeDetailsViewModel> mazeDetailsNavigationService)
        {
            _mazeDetailsStore = mazeDetailsStore;
            _mazeDetailsNavigationService = mazeDetailsNavigationService;
        }

        public override ButtonViewModel GrantCommand(ObservableCollection<CommandViewModel<MazeWithSolutionCanvasViewModel>> collection,
                                                     CommandViewModel<MazeWithSolutionCanvasViewModel> commandViewModel)
        {
            return new ButtonViewModel(true,
                                       "Details",
                                       new ShowMazeDetailsCommand(commandViewModel, _mazeDetailsStore, _mazeDetailsNavigationService));
        }
    }
}