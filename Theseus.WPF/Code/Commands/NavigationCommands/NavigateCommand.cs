using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;

namespace Theseus.WPF.Code.Commands.NavigationCommands
{
    /// <summary>
    /// The <c>NavigateCommand</c> class changes the active view.
    /// </summary>
    /// <typeparam name="TViewModel">View model assiociated with the chosen View.</typeparam>
    public class NavigateCommand<TViewModel> : CommandBase where TViewModel : ViewModelBase
    {
        private readonly NavigationService<TViewModel> _navigationService;

        public NavigateCommand(NavigationService<TViewModel> navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            _navigationService.Navigate();
        }
    }
}