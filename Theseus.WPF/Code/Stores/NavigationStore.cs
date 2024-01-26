using System;
using Theseus.WPF.Code.Bases;

namespace Theseus.WPF.Code.Stores
{
    /// <summary>
    /// The <c>NavigationStore</c> class contains currently used view model.
    /// </summary>
    public class NavigationStore
    {
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;

            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        public event Action CurrentViewModelChanged;

        private void OnCurrentViewModelChanged() => CurrentViewModelChanged?.Invoke();
    }
}