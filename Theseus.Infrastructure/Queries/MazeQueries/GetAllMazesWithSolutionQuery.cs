using AutoMapper;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.MazeQueries
{
    public class GetAllMazesWithSolutionQuery : IGetAllMazesWithSolutionQuery
    {
        private readonly TheseusDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        public GetAllMazesWithSolutionQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public IEnumerable<MazeWithSolution> GetAllMazesWithSolution()
        {
            using (TheseusDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<MazeDto> mazeEntities = context.Mazes.ToList();

                List<MazeWithSolution> mazes = new List<MazeWithSolution>();
                foreach(var mazeEntity in mazeEntities)
                {
                    MazeWithSolution mazeWithSolution = _mapper.Map<MazeWithSolution>(mazeEntity);
                    mazeWithSolution.StaffMember = _mapper.Map<StaffMember>(mazeEntity.Owner);
                    mazes.Add(mazeWithSolution);
                }
                return mazes;
            }
        }
    }
}