using System.Collections.ObjectModel;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.ViewModels
{
    public abstract class CommandListViewModel<TModel> : ViewModelBase
    {
        public SelectedModelListStore<TModel> SelectedModelListStore { get; }
        public ObservableCollection<CommandViewModel<TModel>> ActionableModels { get; } = new ObservableCollection<CommandViewModel<TModel>>();

        public CommandListViewModel(SelectedModelListStore<TModel> selectedModelListStore)
        {
            SelectedModelListStore = selectedModelListStore;
        }

        public void CreateModelCommandViewModels()
        {
            this.ActionableModels.Clear();

            foreach (var model in SelectedModelListStore.ModelList)
            {
                AddModelToActionableModels(model);
            }
        }

        protected abstract void AddModelToActionableModels(TModel model);
    }
}