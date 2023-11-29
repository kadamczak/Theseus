using System.Windows.Input;
using Theseus.WPF.Code.Bases;

namespace Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel
{
    public class ButtonViewModel : ViewModelBase
    {
        public ICommand Command { get; set; }

        public bool Show { get; set; } = false;

        private string _commandName = string.Empty;

        public string CommandName
        {
            get => _commandName;
            set
            {
                _commandName = value;
                OnPropertyChanged(nameof(CommandName));
            }
        }

        public ButtonViewModel(bool show)
        {
            Show = show;
        }

        public ButtonViewModel(bool show, string commandName)
        {
            Show = show;
            CommandName = commandName;
        }

        public ButtonViewModel(bool show, string commandName, ICommand command)
        {
            Show = show;
            CommandName = commandName;
            Command = command;
        }
    }
}
