using Theseus.Code.Bases;
using Theseus.Code.Services;

namespace Theseus.Code.Commands
{
    public class NavigateCommand : CommandBase
    {
        private readonly NavigationService _navigationService;

        public NavigateCommand(NavigationService navigationService)
        {
            _navigationService = navigationService;   
        }

        public override void Execute(object? parameter)
        {
            _navigationService.Navigate();
        }
    }
}