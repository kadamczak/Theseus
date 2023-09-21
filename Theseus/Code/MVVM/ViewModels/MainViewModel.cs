using Theseus.Code.Bases;

namespace Theseus.Code.MVVM.ViewModels
{
    public class MainViewModel : Bases.ViewModel
    {
        public ViewModel CurrentViewModel { get; set; }

        public MainViewModel()
        {
            CurrentViewModel = new MazeGeneratorSettingsViewModel();
        }

    }
}
