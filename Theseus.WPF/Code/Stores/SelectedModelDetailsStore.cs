namespace Theseus.WPF.Code.Stores
{
    /// <summary>
    /// The <c>SelectedModelDetailsStore</c> generic class stores the currently selected model
    /// used for detail views etc.
    /// Multiple stores with varying types exist at once in the application.
    /// </summary>
    /// <typeparam name="TModel">Type of model stored.</typeparam>
    public class SelectedModelDetailsStore<TModel>
    {
        public TModel SelectedModel { get; set; }
    }
}