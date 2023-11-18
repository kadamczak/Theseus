using AutoMapper;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.Models.UserRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.MazeQueries
{
    public abstract class MazeQuery : Query
    {
        protected MazeQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        protected List<MazeWithSolution> GetMazesWithSolution(TheseusDbContext context, IEnumerable<MazeDto> mazeDtos, bool loadOwner, bool loadExamSets)
        {
            List<MazeWithSolution> mazes = new List<MazeWithSolution>();
            foreach (var mazeDto in mazeDtos)
            {
                mazes.Add(GetMazeWithSolution(context, mazeDto, loadOwner, loadExamSets));
            }
            return mazes;
        }

        protected MazeWithSolution GetMazeWithSolution(TheseusDbContext context, MazeDto mazeDto, bool loadOwner, bool loadExamSets)
        {
            if (loadOwner)
                context.Entry(mazeDto).Reference(p => p.Owner).Load();

            if (loadExamSets)
                context.Entry(mazeDto).Collection(p => p.ExamSetDtos).Load();

            return MapToMazeWithSolution(mazeDto);
        }

        protected MazeWithSolution MapToMazeWithSolution(MazeDto mazeDto)
        {
            MazeWithSolution mazeWithSolution = Mapper.Map<MazeWithSolution>(mazeDto);
            mazeWithSolution.StaffMember = Mapper.Map<StaffMember>(mazeDto.Owner);

            if (mazeDto.ExamSetDtos is null)
                return mazeWithSolution;

            foreach (var examSet in mazeDto.ExamSetDtos)
            {
                mazeWithSolution.ExamSets.Add(Mapper.Map<ExamSet>(examSet));
            }

            return mazeWithSolution;
        }
    }
}
