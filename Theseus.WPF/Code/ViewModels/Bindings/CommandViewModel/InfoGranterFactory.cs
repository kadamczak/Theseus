using System;

namespace Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel
{
    public abstract class InfoGranterFactory<TModel, TEnumInfo>
        where TModel : class
        where TEnumInfo : struct, IConvertible
    {
        public abstract InfoGranter<TModel> Create(TEnumInfo chosenInfoType);
    }
}