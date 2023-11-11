using System.ComponentModel;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.SettingsCommands
{
    public class ChangeMinimalCellSizeCommand : CommandBase
    {
        private readonly MinimalCellSizeSetterViewModel _minimalCellSizeSetterViewModel;
        private readonly NavigationService<SettingsViewModel> _navigateToSettings;

        public ChangeMinimalCellSizeCommand(MinimalCellSizeSetterViewModel minimalCellSizeSetterViewModel,
                                            NavigationService<SettingsViewModel> navigateToService)
        {
            _minimalCellSizeSetterViewModel = minimalCellSizeSetterViewModel;
            _navigateToSettings = navigateToService;

            _minimalCellSizeSetterViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }
        protected override void Dispose()
        {
            _minimalCellSizeSetterViewModel.PropertyChanged -= OnViewModelPropertyChanged;
            base.Dispose();
        }

        public override void Execute(object? parameter)
        {
            Properties.Settings.Default.MinimalCellSize = float.Parse(_minimalCellSizeSetterViewModel.MinimalCellSize);
            Properties.Settings.Default.Save();

            _navigateToSettings.Navigate();
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_minimalCellSizeSetterViewModel.MinimalCellSize))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            if (!float.TryParse(_minimalCellSizeSetterViewModel.MinimalCellSize, out float number))
            {
                return false;
            }

            return base.CanExecute(parameter);
        }
    }
}
