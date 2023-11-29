using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;
using Theseus.WPF.Code.ViewModels.MazeViewModels.MazeCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.MazeViewModels.MazeCommandList.Info;

namespace Theseus.WPF.Code.ViewModels
{
    public class MazeCommandListViewModel : CommandListViewModel<MazeWithSolutionCanvasViewModel, MazeButtonCommand, MazeInfo>
    {
        public MazeCommandListViewModel(SelectedModelListStore<MazeWithSolutionCanvasViewModel> selectedModelListStore,
                               CommandGranterFactory<MazeWithSolutionCanvasViewModel, MazeButtonCommand> commandGranterFactory,
                               InfoGranterFactory<MazeWithSolutionCanvasViewModel, MazeInfo> infoGranterFactory,
                               MazeButtonCommand command1,
                               MazeButtonCommand command2,
                               MazeInfo info) : base(selectedModelListStore, commandGranterFactory, infoGranterFactory, command1, command2, info)
        {
        }
    }
}