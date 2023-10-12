using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.Models.MazeRelated.MazeGenerators;
using Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators;

namespace Theseus.Domain.Models.MazeRelated.MazeCreators
{
    public class MazeCreator
    {
        private readonly MazeStructureGeneratorFactory _mazeStructureGeneratorFactory;
        private readonly MazeSolutionGeneratorFactory _mazeSolutionGeneratorFactory;

        public MazeCreator(MazeStructureGeneratorFactory mazeStructureGeneratorFactory,
                           MazeSolutionGeneratorFactory mazeSolutionGeneratorFactory)
        {
            this._mazeStructureGeneratorFactory = mazeStructureGeneratorFactory;
            this._mazeSolutionGeneratorFactory = mazeSolutionGeneratorFactory;
        }

        public MazeWithSolution CreateMaze(int height, int width, MazeStructureGenAlgorithm algorithm)
        {
            MazeWithSolution maze = new MazeWithSolution(height, width);
            GenerateMazeStructure(maze, algorithm);
            return maze;
        }

        public MazeWithSolution CreateMazeWithSolution(int height,
                                                       int width,
                                                       MazeStructureGenAlgorithm structureAlgorithm,
                                                       MazeSolutionGenAlgorithm solutionAlgorithm)
        {
            MazeWithSolution maze = CreateMaze(height, width, structureAlgorithm);

            MazeWithSolution mazeWithSolution = new MazeWithSolution(maze);
            GenerateMazeSolution(mazeWithSolution, solutionAlgorithm);
            return mazeWithSolution;
        }

        private void GenerateMazeStructure(MazeWithSolution grid, MazeStructureGenAlgorithm algorithm)
        {
            var generator = _mazeStructureGeneratorFactory.Create(algorithm);
            generator.GenerateMazeStructureInGrid(grid);
        }

        private void GenerateMazeSolution(MazeWithSolution maze, MazeSolutionGenAlgorithm algorithm)
        {
            var generator = _mazeSolutionGeneratorFactory.Create(algorithm);
            generator.GenerateSolutionInMaze(maze);
        }
    }
}
