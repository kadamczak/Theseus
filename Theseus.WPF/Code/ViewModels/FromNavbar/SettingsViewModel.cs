using System.Windows.Input;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.NavigationCommands;
using Theseus.WPF.Code.Commands.SettingsCommands;
using Theseus.WPF.Code.Services;

namespace Theseus.WPF.Code.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private string _chosenLanguage;
        public string ChosenLanguage
        {
            get => _chosenLanguage;
            set
            {
                _chosenLanguage = value;
                OnPropertyChanged(nameof(ChosenLanguage));
            }
        }
        
        private float _minimalCellSize;
        public float MinimalCellSize
        {
            get => _minimalCellSize;
            set
            {
                _minimalCellSize = value;
                OnPropertyChanged(nameof(MinimalCellSize));
            }
        }
        
        public ICommand ChangeLanguage { get; }
        public ICommand NavigateToMinimalCellSizeSetter { get; }

        public SettingsViewModel(NavigationService<MinimalCellSizeSetterViewModel> minimalCellSizeSetterNavigationService)
        {
            ChosenLanguage = Properties.Settings.Default.AppLanguage;
            MinimalCellSize = Properties.Settings.Default.MinimalCellSize;

            ChangeLanguage = new ChangeLanguageCommand(this, new LoadStringResourcesCommand());
            NavigateToMinimalCellSizeSetter = new NavigateCommand<MinimalCellSizeSetterViewModel>(minimalCellSizeSetterNavigationService);
        }
    }
}
