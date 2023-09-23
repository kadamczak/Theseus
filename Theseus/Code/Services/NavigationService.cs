using System;
using Theseus.Code.Bases;
using Theseus.Code.Stores;

namespace Theseus.Code.Services
{
    public class NavigationService
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<ViewModel> _createViewModel;

        public NavigationService(NavigationStore navigationStore, Func<ViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }

    }
}
