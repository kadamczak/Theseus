using System;

namespace Theseus.WPF.Code.Stores.Mazes
{
    public class NavigationEnabledStore
    {
        private bool _navigationEnabled = true;
        public bool NavigationEnabled
        {
            get => _navigationEnabled;
            set
            {
                _navigationEnabled = value;
                NavigationEnabledChanged?.Invoke();
            }
        }

        public event Action NavigationEnabledChanged;
    }
}