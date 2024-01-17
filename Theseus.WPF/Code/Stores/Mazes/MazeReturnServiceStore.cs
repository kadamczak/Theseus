using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;

namespace Theseus.WPF.Code.Stores.Mazes
{
    /// <summary>
    /// The <c>MazeReturnServiceStore</c> class stores the most recently chosen <c>MazeWithSolution</c> return service.
    /// </summary>
    public class MazeReturnServiceStore
    {
        public NavigationService<ViewModelBase> MazeReturnNavigationService { get; set; }
    }
}