using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace Theseus.Code.MVVM.ViewModels
{
    public class MazeGeneratorSettingsViewModel : Bases.ViewModel
    {
        private ListBoxItem? _selectedAlgorithm;
        private string _algorithmDescription = string.Empty;

        public ListBoxItem? SelectedAlgorithm
        {
            get => _selectedAlgorithm;
            set
            {
                _selectedAlgorithm = value;
                OnPropertyChanged(nameof(SelectedAlgorithm));
            }
        }

        public string AlgorithmDescription
        {
            get => _algorithmDescription;
            set
            {
                _algorithmDescription = value;
                OnPropertyChanged(nameof(AlgorithmDescription));
            }
        }

        public MazeGeneratorSettingsViewModel()
        {
            PropertyChanged += HandlePropertyChange;
        }

        public void HandlePropertyChange(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(SelectedAlgorithm))
            {
                UpdateAlgorithmDescription();
            }

        }

        private void UpdateAlgorithmDescription()
        {
            if (SelectedAlgorithm is null)
                return;

            string chosenAlgorithm = (string)SelectedAlgorithm.Content;
            AlgorithmDescription = (string)(App.Current.Resources[chosenAlgorithm]);
        }


    }
}
