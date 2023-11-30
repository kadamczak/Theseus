using Theseus.WPF.Code.Bases;

namespace Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel
{
    public class CommandViewModel<T> : ViewModelBase
    {
        public T Model { get; set; }

        public ButtonViewModel Button1 { get; set; }
        public ButtonViewModel Button2 { get; set; }

        private string _info = string.Empty;
        public string Info
        {
            get => _info;
            set
            {
                _info = value;
                OnPropertyChanged(nameof(Info));
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