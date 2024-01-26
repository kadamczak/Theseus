using System;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores;

namespace Theseus.WPF.Code.Services
{
    /// <summary>
    /// The <c>NavigationService</c> class has the ability to change the currently selected page view model.
    /// </summary>
    /// <typeparam name="TViewModel">Type of view model.</typeparam>
    public class NavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public NavigationService(NavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate() => _navigationStore.CurrentViewModel = _createViewModel();
    }
}