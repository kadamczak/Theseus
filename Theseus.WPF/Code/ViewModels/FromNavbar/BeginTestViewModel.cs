using System;
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
        public ExamMazeCanvasViewModel ExamMazeCanvasViewModel { get; }
        public IGetMazeWithSolutionByIdQuery GetMazeById { get; }

        public BeginTestViewModel(TheseusDbContextFactory factory, MazeDtoToMazeWithSolutionConverter converter)
        {
            Guid guid = new Guid("b879799d-af75-4ce4-734d-08dbd14fb5c2");
            GetMazeById = new GetMazeWithSolutionByIdQuery(factory, converter);
            MazeWithSolution m = GetMazeById.GetMazeWithSolutionById(guid);
            ExamMazeCanvasViewModel = new ExamMazeCanvasViewModel(m);
        }
    }
}