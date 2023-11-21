using System.Timers;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.ViewModels.Components;

namespace Theseus.WPF.Code.ViewModels
{
    public class ExamPageViewModel : ViewModelBase
    {
        public ExamMazeCanvasViewModel ExamMazeCanvasViewModel { get; }
        public IGetMazeWithSolutionByIdQuery GetMazeById { get; }

        private int _countdownValue = 3;
        public int CountdownValue
        {
            get => _countdownValue;
            set
            {
                _countdownValue = value;
                OnPropertyChanged(nameof(CountdownValue));
            }
        }
        private Timer _transitionTimer = new Timer() { Interval = 1000 };

        //TheseusDbContextFactory factory, IMapper mapper
        public ExamPageViewModel()
        {
            //Guid guid = new Guid("b879799d-af75-4ce4-734d-08dbd14fb5c2");
            //GetMazeById = new GetMazeWithSolutionByIdQuery(factory, mapper);
            //MazeWithSolution? m = GetMazeById.GetMazeWithSolutionById(guid);

            //if (m is null)
            //    throw new Exception("Maze doesn't exist.");

            //ExamMazeCanvasViewModel = new ExamMazeCanvasViewModel(m);

            //ExamMazeCanvasViewModel.CompletedMaze += StartSecondCountdown;
            //_transitionTimer.Elapsed += new ElapsedEventHandler(ReduceCountdownValue);
        }

        private void StartCountdown()
        {
            _transitionTimer.Start();
        }

        private void ReduceCountdownValue(object? sender, ElapsedEventArgs e)
        {
            CountdownValue--;

            if (CountdownValue == 0)
            {
                _transitionTimer.Stop();
            }
        }
    }
}