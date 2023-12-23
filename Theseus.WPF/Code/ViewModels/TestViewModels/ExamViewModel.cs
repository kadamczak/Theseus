using Theseus.WPF.Code.Bases;

namespace Theseus.WPF.Code.ViewModels
{
    public abstract class ExamViewModel : ViewModelBase
    {
        private bool _canGoToNextPage = true;
        public bool CanGoToNextPage
        {
            get => _canGoToNextPage;
            set
            {
                _canGoToNextPage = value;
                OnPropertyChanged(nameof(CanGoToNextPage));
            }
        }
    }
}