using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.ViewModels.SetViewModels;

namespace Theseus.WPF.Code.ViewModels
{
    public class CreateSetViewModel : ViewModelBase
    {
        public SetGeneratorViewModel SetGeneratorViewModel { get; }

        public CreateSetViewModel(SetGeneratorViewModel setGeneratorViewModel)
        {
            this.SetGeneratorViewModel = setGeneratorViewModel;
        }


    }
}