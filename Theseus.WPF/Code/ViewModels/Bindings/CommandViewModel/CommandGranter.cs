using System.Collections.ObjectModel;

namespace Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel
{
    public abstract class CommandGranter<TModel> where TModel : class
    {
        public abstract ButtonViewModel GrantCommand(ObservableCollection<CommandViewModel<TModel>> collection, CommandViewModel<TModel> commandViewModel);
    }
}