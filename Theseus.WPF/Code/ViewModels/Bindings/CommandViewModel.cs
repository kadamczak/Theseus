using System.Windows.Input;
using Theseus.WPF.Code.Bases;

namespace Theseus.WPF.Code.ViewModels.Bindings
{
    public class CommandViewModel<T> : ViewModelBase
    {
        public T Model { get; set; }

        public bool ShowCommand1 { get; set; } = false;
        public bool ShowCommand2 { get; set; } = false;

        public ICommand Command1 { get; set; }
        public ICommand Command2 { get; set; }

        private string _command1Name = string.Empty;
        private string _command2Name = string.Empty;

        public string Command1Name
        {
            get => _command1Name;
            set
            {
                _command1Name = value;
                OnPropertyChanged(nameof(Command1Name));
            }
        }
        public string Command2Name
        {
            get => _command2Name;
            set
            {
                _command2Name = value;
                OnPropertyChanged(nameof(Command2Name));
            }
        }

        private bool _selected = false;
        public bool Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                OnPropertyChanged(nameof(Selected));
            }
        }

        public CommandViewModel(T model)
        {
            Model = model;
        }
    }
}
