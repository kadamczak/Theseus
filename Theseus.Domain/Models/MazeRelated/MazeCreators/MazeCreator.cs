using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.Models.MazeRelated.MazeGenerators;
using Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators;

namespace Theseus.Domain.Models.MazeRelated.MazeCreators
{
    public class MazeCreator
    {
        public Maze CreateMaze(int height, int width, MazeStructureGenAlgorithm algorithm)
        {
            Maze maze = new Maze(height, width);
            GenerateMazeStructure(maze, algorithm);
            return maze;
        }

        public MazeWithSolution CreateMazeWithSolution(int height,
                                                       int width,
                                                       MazeStructureGenAlgorithm structureAlgorithm,
                                                       MazeSolutionGenAlgorithm solutionAlgorithm)
        {
            Maze maze = CreateMaze(height, width, structureAlgorithm);

            MazeWithSolution mazeWithSolution = new MazeWithSolution(maze);
            GenerateMazeSolution(mazeWithSolution, solutionAlgorithm);
            return mazeWithSolution;
        }

        private void GenerateMazeStructure(Maze grid, MazeStructureGenAlgorithm algorithm)
        {
            var generator = MazeStructureGeneratorFactory.Create(algorithm);
            generator.GenerateMazeStructureInGrid(grid);
        }

        private void GenerateMazeSolution(MazeWithSolution maze, MazeSolutionGenAlgorithm algorithm)
        {
            var generator = MazeSolutionGeneratorFactory.Create(algorithm);
            generator.GenerateSolutionInMaze(maze);
        }
    }
}
