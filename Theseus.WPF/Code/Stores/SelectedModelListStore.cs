using System.Collections.Generic;

namespace Theseus.WPF.Code.Stores
{
    /// <summary>
    /// The <c>SelectedModelListStore</c> generic class stores the currently selected list of models
    /// used for list views etc.
    /// Multiple list stores with varying types exist at once in the application.
    /// </summary>
    /// <typeparam name="TModel">Type of models stored in list.</typeparam>
    public class SelectedModelListStore<TModel>
    {
        public IEnumerable<TModel> ModelList { get; set; }
    }
}