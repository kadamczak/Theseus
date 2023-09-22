using Theseus.Code.MVVM.Models.Maze.Enums;

namespace Theseus.Code.MVVM.ViewModels.Bindings
{
    public class AlgorithmViewModel
    {
        public string DisplayName { get; }
        public MazeGenAlgorithm Algorithm { get; }

        public AlgorithmViewModel(string name, MazeGenAlgorithm alg)
        {
            DisplayName = name;
            Algorithm = alg;
        }
    }

}
