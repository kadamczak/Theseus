using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Infrastructure.Dtos.Converters
{
    public class CollectionConverter : ValueConverter<ICollection<MazeWithSolution>, ICollection<MazeDto>>
    {
        public CollectionConverter(MazeWithSolutionToMazeDtoConverter toMazeDto,
                                   MazeDtoToMazeWithSolutionConverter toMazeWithSolution)
            : base(values => ToMazesDtos(values), values => ToMazesWithSolution(values))
        {
        }

        private static ICollection<MazeDto> ToMazesDtos(ICollection<MazeWithSolution> collection)
        {
            MazeWithSolutionToMazeDtoConverter toMazeDtos = new MazeWithSolutionToMazeDtoConverter();
            List<MazeDto> mazeDtos = new List<MazeDto>();

            foreach (var maze in collection)
            {
                mazeDtos.Add(toMazeDtos.Convert(maze));
            }

            return mazeDtos;
        }

        private static ICollection<MazeWithSolution> ToMazesWithSolution(ICollection<MazeDto> collection)
        {
            MazeDtoToMazeWithSolutionConverter toMazeWithSolution = new MazeDtoToMazeWithSolutionConverter();
            List<MazeWithSolution> mazes = new List<MazeWithSolution>();

            foreach (var mazeDto in collection)
            {
                mazes.Add(toMazeWithSolution.Convert(mazeDto));
            }

            return mazes;
        }

    }
}