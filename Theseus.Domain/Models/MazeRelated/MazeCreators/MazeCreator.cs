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

        public Maze CreateMaze(int height, int width, MazeStructureGenAlgorithm algorithm, int? rndSeed = null)
        {
            Maze maze = new Maze(height, width);
            GenerateMazeStructure(maze, algorithm, rndSeed);
            return maze;
        }

        public MazeWithSolution CreateMazeWithSolution(int height,
                                                       int width,
                                                       MazeStructureGenAlgorithm structureAlgorithm,
                                                       MazeSolutionGenAlgorithm solutionAlgorithm,
                                                       bool shouldExcludeCellsCloseToRoot,
                                                       int? rndSeed = null)
        {
            Maze maze = CreateMaze(height, width, structureAlgorithm, rndSeed);

            MazeWithSolution mazeWithSolution = new MazeWithSolution(maze);
            GenerateMazeSolution(mazeWithSolution, solutionAlgorithm, shouldExcludeCellsCloseToRoot);
            return mazeWithSolution;
        }

        private void GenerateMazeStructure(Maze grid, MazeStructureGenAlgorithm algorithm, int? rndSeed = null)
        {
            var generator = _mazeStructureGeneratorFactory.Create(algorithm);
            generator.GenerateMazeStructureInGrid(grid, rndSeed);
        }

        private void GenerateMazeSolution(MazeWithSolution maze, MazeSolutionGenAlgorithm algorithm, bool shouldExcludeCellsCloseToRoot, int? rndSeed = null)
        {
            var generator = _mazeSolutionGeneratorFactory.Create(algorithm, shouldExcludeCellsCloseToRoot);
            generator.GenerateSolutionInMaze(maze, rndSeed);
        }
    }
}