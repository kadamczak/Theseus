using Theseus.Code.Bases;
using Theseus.Code.MVVM.Models.Maze.Enums;
using Theseus.Code.MVVM.Models.Maze.Generators;
using Theseus.Code.MVVM.Models.Maze.GridStructure;

namespace Theseus.Code.Commands
{
    public class GenerateMazeCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            var parameters = new GenerateMazeCommandParameters(parameter);

            MazeGenerator generator = MazeGeneratorFactory.Create(parameters.Algorithm);

            Grid maze = generator.GenerateMaze(parameters.Width, parameters.Height);
        }

    }
}
