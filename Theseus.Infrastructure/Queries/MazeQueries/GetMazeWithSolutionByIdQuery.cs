using AutoMapper;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;
using Theseus.Infrastructure.Dtos.Converters.MazeConverters;

namespace Theseus.Infrastructure.Queries.MazeQueries
{
    public class GetMazeWithSolutionByIdQuery : IGetMazeWithSolutionByIdQuery
    {
        private readonly TheseusDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        public GetMazeWithSolutionByIdQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public MazeWithSolution? GetMazeWithSolutionById(Guid id)
        {
            using (TheseusDbContext context = _dbContextFactory.CreateDbContext())
            {
                MazeDto? mazeEntity = context.Mazes.Find(id);

                if(mazeEntity is null)
                {
                    return null;
                }
                else
                {
                    MazeWithSolution mazeWithSolution = _mapper.Map<MazeWithSolution>(mazeEntity);
                    mazeWithSolution.StaffMember = _mapper.Map<StaffMember>(mazeEntity.Owner);
                    return mazeWithSolution;
                }
            }
        }
    }
}