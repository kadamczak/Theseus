using Theseus.Domain.Models.ExamSetRelated;
using Theseus.WPF.Code.Stores;

namespace Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList
{
    public class AddToGroupExamSetCommandListViewModel : CommandListViewModel<ExamSet>
    {
        public AddToGroupExamSetCommandListViewModel(SelectedModelListStore<ExamSet> selectedExamSetDetailsStore) : base(selectedExamSetDetailsStore)
        {
        }

        public override void AddModelToActionableModels(ExamSet examSet)
        {
            //ExamSetCommandViewModel examSetCommandViewModel = new ExamSetCommandViewModel(examSet)
            //{
            //    Command1Name = "Details",
            //    ShowCommand2 = false
            //};
            //examSetCommandViewModel.Command1 = new ShowExamSetDetailsCommand(examSetCommandViewModel,
            //                                                                 _selectedExamSetDetailsStore,
            //                                                                 _navigationService);
            //this.ActionableExamSets.Add(examSetCommandViewModel);
        }
    }
}
