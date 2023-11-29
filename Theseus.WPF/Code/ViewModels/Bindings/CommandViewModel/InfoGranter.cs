namespace Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel
{
    public abstract class InfoGranter<TModel> where TModel : class
    {
        public abstract string GrantInfo(CommandViewModel<TModel> commandViewModel);
    }
}