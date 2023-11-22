using Theseus.Domain.Models.ExamSetRelated;
using Theseus.WPF.Code.Commands.ExamSetCommands;
using Theseus.WPF.Code.Stores.ExamSets;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList
{
    public class AddToGroupExamSetCommandListViewModel : ExamSetCommandListViewModel
    {
        public AddToGroupExamSetCommandListViewModel(SelectedExamSetListStore selectedExamSetDetailsStore) : base(selectedExamSetDetailsStore)
        {
        }

        protected override void AddExamSetToActionableExamSets(ExamSet examSet)
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
