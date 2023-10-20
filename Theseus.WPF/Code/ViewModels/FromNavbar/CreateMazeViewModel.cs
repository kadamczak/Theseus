using Theseus.WPF.Code.Bases;

namespace Theseus.WPF.Code.ViewModels
{
    public class CreateMazeViewModel : ViewModelBase
    {
        public MazeGeneratorViewModel MazeGeneratorViewModel { get; }

        public CreateMazeViewModel(MazeGeneratorViewModel mazeGeneratorViewModel)
        {
            MazeGeneratorViewModel = mazeGeneratorViewModel;
        }
    }
}