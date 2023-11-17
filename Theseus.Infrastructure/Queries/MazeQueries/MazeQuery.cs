using AutoMapper;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.Models.UserRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.MazeQueries
{
    public abstract class MazeQuery
    {
        protected TheseusDbContextFactory DbContextFactory { get; }
        protected IMapper Mapper { get; }

        public MazeQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper)
        {
            DbContextFactory = dbContextFactory;
            Mapper = mapper;
        }

        protected MazeWithSolution MapToMazeWithSolution(MazeDto mazeDto)
        {
            MazeWithSolution mazeWithSolution = Mapper.Map<MazeWithSolution>(mazeDto);
            mazeWithSolution.StaffMember = Mapper.Map<StaffMember>(mazeDto.Owner);
            return mazeWithSolution;
        }

        protected List<MazeWithSolution> MapToMazeWithSolution(IEnumerable<MazeDto> mazeDtos)
        {
            List<MazeWithSolution> mazes = new List<MazeWithSolution>();
            foreach (var mazeDto in mazeDtos)
            {
                mazes.Add(MapToMazeWithSolution(mazeDto));
            }
            return mazes;
        }
    }
}
