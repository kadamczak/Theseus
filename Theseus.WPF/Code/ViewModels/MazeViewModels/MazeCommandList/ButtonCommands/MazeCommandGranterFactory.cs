using System;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;
using Theseus.WPF.Code.ViewModels.MazeViewModels.MazeCommandList.ButtonCommands.Implementations;

namespace Theseus.WPF.Code.ViewModels.MazeViewModels.MazeCommandList.ButtonCommands
{
    public class MazeCommandGranterFactory : CommandGranterFactory<MazeWithSolutionCanvasViewModel, MazeButtonCommand>
    {
        private readonly ShowDetailsMazeCommandGranter _showDetailsGranter;
        private readonly DeleteMazeCommandGranter _deleteMazeGranter;
        private readonly AddToExamSetCommandGranter _addToExamSetGranter;
        private readonly EmptyMazeCommandGranter _emptyGranter;

        public MazeCommandGranterFactory(ShowDetailsMazeCommandGranter showDetailsGranter,
                                         DeleteMazeCommandGranter deleteMazeGranter,
                                         AddToExamSetCommandGranter addToExamSetGranter,
                                         EmptyMazeCommandGranter emptyGranter)
        {
            _showDetailsGranter = showDetailsGranter;
            _deleteMazeGranter = deleteMazeGranter;
            _addToExamSetGranter = addToExamSetGranter;
            _emptyGranter = emptyGranter;
        }

        public override CommandGranter<MazeWithSolutionCanvasViewModel> Get(MazeButtonCommand chosenCommandType)
        {
            return chosenCommandType switch
            {
                MazeButtonCommand.ShowDetails => _showDetailsGranter,
                MazeButtonCommand.Delete => _deleteMazeGranter,
                MazeButtonCommand.AddToExamSet => _addToExamSetGranter,
                MazeButtonCommand.None => _emptyGranter,
                _ => throw new ArgumentException("Invalid maze command type.")
            };
        }
    }
}