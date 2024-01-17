using System;
using System.Collections.ObjectModel;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels
{
    /// <summary>
    /// The <c>CommandListViewModel</c> generic class defines standard command list functionality.
    /// </summary>
    public class CommandListViewModel<TModel, TEnumCommand, TEnumInfo> : ViewModelBase
        where TModel : class
        where TEnumCommand : struct, IConvertible
        where TEnumInfo : struct, IConvertible
    {
        public SelectedModelListStore<TModel> SelectedModelListStore { get; }
        public ObservableCollection<CommandViewModel<TModel>> ActionableModels { get; } = new ObservableCollection<CommandViewModel<TModel>>();

        protected CommandGranterFactory<TModel, TEnumCommand> _commandGranterFactory;
        protected InfoGranterFactory<TModel, TEnumInfo> _infoGranterFactory;

        private readonly TEnumCommand _command1Type;
        public readonly TEnumCommand _command2Type;
        public readonly TEnumInfo _infoType;

        public CommandListViewModel(SelectedModelListStore<TModel> selectedModelListStore,
                                    CommandGranterFactory<TModel, TEnumCommand> commandGranterFactory,
                                    InfoGranterFactory<TModel, TEnumInfo> infoGranterFactory,
                                    TEnumCommand command1,
                                    TEnumCommand command2,
                                    TEnumInfo info)
        {
            SelectedModelListStore = selectedModelListStore;
            _commandGranterFactory = commandGranterFactory;
            _infoGranterFactory= infoGranterFactory;
            _command1Type = command1;
            _command2Type = command2;
            _infoType = info;
        }

        public void CreateModelCommandViewModels()
        {
            this.ActionableModels.Clear();

            foreach (var model in SelectedModelListStore.ModelList)
            {
                AddModelToActionableModels(model);
            }
        }

        public void AddModelToActionableModels(TModel model)
        {
            var actionableModel = new CommandViewModel<TModel>(model);

            actionableModel.Button1 = GrantCommand(actionableModel, _command1Type);
            actionableModel.Button2 = GrantCommand(actionableModel, _command2Type);
            actionableModel.Info = GrantInfo(actionableModel, _infoType);

            ActionableModels.Add(actionableModel);
        }

        protected ButtonViewModel GrantCommand(CommandViewModel<TModel> model, TEnumCommand commandType)
        {
            var commandGranter = _commandGranterFactory.Get(commandType);
            return commandGranter.GrantCommand(ActionableModels, model);
        }

        protected string GrantInfo(CommandViewModel<TModel> model, TEnumInfo infoType)
        {
            var infoGranter = _infoGranterFactory.Create(infoType);
            return infoGranter.GrantInfo(model);
        }
    }
}