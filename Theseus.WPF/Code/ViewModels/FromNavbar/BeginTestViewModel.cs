using System;
using System.Timers;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.QueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos.Converters;
using Theseus.Infrastructure.Queries;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.ViewModels.Components;

namespace Theseus.WPF.Code.ViewModels
{
    public class BeginTestViewModel : ViewModelBase
    {

        public BeginTestViewModel()
        {
            
        }




        //public ExamMazeCanvasViewModel ExamMazeCanvasViewModel { get; }
        //public IGetMazeWithSolutionByIdQuery GetMazeById { get; }

        //private int _countdownValue = 3;
        //public int CountdownValue
        //{
        //    get => _countdownValue;
        //    set
        //    {
        //        _countdownValue = value;
        //        OnPropertyChanged(nameof(CountdownValue));
        //    }
        //}
        //private Timer _transitionTimer = new Timer() { Interval = 1000 };

        //public BeginTestViewModel(TheseusDbContextFactory factory, MazeDtoToMazeWithSolutionConverter converter)
        //{
        //    Guid guid = new Guid("fbdb43c9-53f8-45df-a5a5-08dbd2638488");
        //    GetMazeById = new GetMazeWithSolutionByIdQuery(factory, converter);
        //    MazeWithSolution m = GetMazeById.GetMazeWithSolutionById(guid);
        //    ExamMazeCanvasViewModel = new ExamMazeCanvasViewModel(m);

        //    ExamMazeCanvasViewModel.CompletedMaze += StartSecondCountdown;
        //    _transitionTimer.Elapsed += new ElapsedEventHandler(ReduceCountdownValue);

        //}

        //private void StartSecondCountdown()
        //{
        //    _transitionTimer.Start();
        //}

        //private void ReduceCountdownValue(object? sender, ElapsedEventArgs e)
        //{
        //    CountdownValue--;

        //    if(CountdownValue == 0)
        //    {
        //        _transitionTimer.Stop();
        //    }
        //}
    }
}