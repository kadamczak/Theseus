using System;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;
using Theseus.WPF.Code.ViewModels.MazeViewModels.MazeCommandList.Info.Implementations;

namespace Theseus.WPF.Code.ViewModels.MazeViewModels.MazeCommandList.Info
{
    public class MazeInfoGranterFactory : InfoGranterFactory<MazeWithSolutionCanvasViewModel, MazeInfo>
    {
        private readonly EmptyMazeInfoGranter _emptyInfoGranter;
        private readonly GeneralMazeInfoGranter _generalInfoGranter;

        public MazeInfoGranterFactory(EmptyMazeInfoGranter emptyInfoGranter, GeneralMazeInfoGranter generalInfoGranter)
        {
            _emptyInfoGranter = emptyInfoGranter;
            _generalInfoGranter = generalInfoGranter;
        }

        public override InfoGranter<MazeWithSolutionCanvasViewModel> Create(MazeInfo chosenInfoType)
        {
            return chosenInfoType switch
            {
                MazeInfo.None => _emptyInfoGranter,
                MazeInfo.GeneralInfo => _generalInfoGranter,
                _ => throw new ArgumentException("Invalid maze info type.")
            };
        }
    }
}