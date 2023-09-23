using System;
using Theseus.Code.Bases;
using Theseus.Code.Stores;

namespace Theseus.Code.MVVM.ViewModels
{
    public class MainViewModel : Bases.ViewModel
    {
        private readonly NavigationStore _navigationStore;

        public ViewModel CurrentViewModel => _navigationStore.CurrentViewModel;
        public ViewModel NavigationBarViewModel { get; }

        public MainViewModel(NavigationStore navigationStore, Func<ViewModel> createNavigationBarViewModel)
        {
            NavigationBarViewModel = createNavigationBarViewModel();
            
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
