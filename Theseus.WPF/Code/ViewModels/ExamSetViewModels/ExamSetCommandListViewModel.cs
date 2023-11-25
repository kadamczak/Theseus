//using System.Collections.ObjectModel;
//using Theseus.Domain.Models.ExamSetRelated;
//using Theseus.WPF.Code.Bases;
//using Theseus.WPF.Code.Stores.ExamSets;
//using Theseus.WPF.Code.ViewModels.Bindings;

//namespace Theseus.WPF.Code.ViewModels
//{
//    public abstract class ExamSetCommandListViewModel : ViewModelBase
//    {
//        public SelectedExamSetListStore SelectedExamSetListStore { get; }
//        public ObservableCollection<CommandViewModel<ExamSet>> ActionableExamSets { get; } = new ObservableCollection<CommandViewModel<ExamSet>>();

//        //public SelectedExamSetsStore SelectedExamSetsStore { get; }

//        public ExamSetCommandListViewModel(SelectedExamSetListStore selectedExamSetDetailsStore)
//        {
//            SelectedExamSetListStore = selectedExamSetDetailsStore;
//        }

//        public void CreateExamSetCommandViewModels()
//        {
//            this.ActionableExamSets.Clear();

//            foreach (var examSet in SelectedExamSetListStore.ExamSets)
//            {
//                AddExamSetToActionableExamSets(examSet);
//            }
//        }

//        protected abstract void AddExamSetToActionableExamSets(ExamSet examSet);
//    }
//}