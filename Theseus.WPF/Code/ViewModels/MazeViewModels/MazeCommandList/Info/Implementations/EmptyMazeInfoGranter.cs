using System;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.MazeViewModels.MazeCommandList.Info.Implementations
{
    public class EmptyMazeInfoGranter : InfoGranter<MazeWithSolutionCanvasViewModel>
    {
        public override string GrantInfo(CommandViewModel<MazeWithSolutionCanvasViewModel> commandViewModel)
        {
            return string.Empty;
        }
    }
}
