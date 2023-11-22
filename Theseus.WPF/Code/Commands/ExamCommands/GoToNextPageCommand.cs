using System.Linq;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Exams;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.ExamCommands
{
    public class GoToNextPageCommand : CommandBase
    {
        private readonly CurrentExamStore _currentExamStore;
        private readonly NavigationService<ExamTransitionViewModel> _examTransitionNavigationService;
        private readonly NavigationService<ExamEndViewModel> _examEndNavigationService;

        public GoToNextPageCommand(CurrentExamStore currentExamStore,
                                   NavigationService<ExamTransitionViewModel> examTransitionNavigationService,
                                   NavigationService<ExamEndViewModel> examEndNavigationService)
        {
            _currentExamStore = currentExamStore;
            _examTransitionNavigationService = examTransitionNavigationService;
            _examEndNavigationService = examEndNavigationService;
        }

        public override void Execute(object? parameter)
        {
            if(LastMazeFinished())
            {
                _examEndNavigationService.Navigate();
            }
            else
            {
                _currentExamStore.CurrentIndex++;
                _examTransitionNavigationService.Navigate();
            }
        }

        private bool LastMazeFinished() => _currentExamStore.CurrentIndex == _currentExamStore.Mazes.Count() - 1;
    }
}