using System.Windows.Input;
using Theseus.Domain.ExamSetCommandInterfaces;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.ExamSetCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.Stores.Mazes;

namespace Theseus.WPF.Code.ViewModels
{
    public class CreateSetManuallyViewModel : ViewModelBase
    {
        public AddToSetMazeCommandListViewModel AddToSetMazeCommandListViewModel { get; }
        public ICommand CreateSetManually { get; }

        public CreateSetManuallyViewModel(SelectedMazeListStore mazeListStore,
                                          IGetAllMazesWithSolutionQuery getAllMazesWithSolutionQuery,
                                          ICreateExamSetCommand createExamSetCommand,
                                          ICurrentStaffMemberStore currentStaffMemberStore,
                                          NavigationService<CreateSetViewModel> createSetNavigationService,
                                          AddToSetMazeCommandListViewModel addToSetMazeCommandListViewModel)
        {
            LoadFullMazeListToStore(getAllMazesWithSolutionQuery, mazeListStore);
            this.CreateSetManually = new CreateExamSetManuallyCommand(addToSetMazeCommandListViewModel, createExamSetCommand, currentStaffMemberStore, createSetNavigationService);

            this.AddToSetMazeCommandListViewModel = addToSetMazeCommandListViewModel;
            this.AddToSetMazeCommandListViewModel.LoadMazesFromMazeListStore();
        }

        private void LoadFullMazeListToStore(IGetAllMazesWithSolutionQuery getAllMazesWithSolutionQuery, SelectedMazeListStore mazeListStore)
        {
            var fullMazeList = getAllMazesWithSolutionQuery.GetAllMazesWithSolution();
            mazeListStore.MazesInList = fullMazeList;
        }
    }
}