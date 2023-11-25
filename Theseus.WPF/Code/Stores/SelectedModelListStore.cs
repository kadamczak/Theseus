using System.Collections.Generic;

namespace Theseus.WPF.Code.Stores
{
    public class SelectedModelListStore<TModel>
    {
        public IEnumerable<TModel> ModelList { get; set; }
    }
}