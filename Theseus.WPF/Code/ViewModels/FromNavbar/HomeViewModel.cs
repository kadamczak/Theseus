using System.Windows.Input;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.SettingsCommands;

namespace Theseus.WPF.Code.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public ICommand ViewCredits { get; }

        public HomeViewModel()
        {
            ViewCredits = new ViewCreditsCommand();
        }
    }
}