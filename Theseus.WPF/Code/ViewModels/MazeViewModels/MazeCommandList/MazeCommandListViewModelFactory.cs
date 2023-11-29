using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.MazeViewModels.MazeCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.MazeViewModels.MazeCommandList.Info;

namespace Theseus.WPF.Code.ViewModels.MazeViewModels.MazeCommandList
{
    public class MazeCommandListViewModelFactory
    {
        private readonly SelectedModelListStore<MazeWithSolutionCanvasViewModel> _selectedListStore;
        private readonly MazeCommandGranterFactory _commandGranterFactory;
        private readonly MazeInfoGranterFactory _infoGranterFactory;

        public MazeCommandListViewModelFactory(SelectedModelListStore<MazeWithSolutionCanvasViewModel> selectedListStore,
                                      MazeCommandGranterFactory commandGranterFactory,
                                      MazeInfoGranterFactory infoGranterFactory)
        {
            _selectedListStore = selectedListStore;
            _commandGranterFactory = commandGranterFactory;
            _infoGranterFactory = infoGranterFactory;
        }

        public MazeCommandListViewModel Create(MazeButtonCommand command1Type, MazeButtonCommand command2Type, MazeInfo infoType)
        {
            return new MazeCommandListViewModel(_selectedListStore, _commandGranterFactory, _infoGranterFactory, command1Type, command2Type, infoType);
        }
    }
}