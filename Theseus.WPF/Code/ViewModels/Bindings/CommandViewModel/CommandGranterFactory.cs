using System;

namespace Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel
{
    public abstract class CommandGranterFactory<TModel, TEnumCommand>
        where TModel : class
        where TEnumCommand : struct, IConvertible
    {
        public abstract CommandGranter<TModel> Get(TEnumCommand chosenCommandType);
    }
}