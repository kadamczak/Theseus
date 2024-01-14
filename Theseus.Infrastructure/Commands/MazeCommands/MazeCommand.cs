using AutoMapper;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.MazeCommands
{
    /// <summary>
    /// Abstract class defining attachment of related entities to the <c>MazeDto</c>.
    /// </summary>
    public abstract class MazeCommand : Command
    {
        protected MazeCommand(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        protected void AttachRelatedEntities(MazeDto mazeDto, TheseusDbContext context)
        {
            if (mazeDto.Owner is not null)
                context.Attach(mazeDto.Owner);

            if (mazeDto.ExamSetDto_MazeDto is not null)
            {
                foreach (var examSetIndex in mazeDto.ExamSetDto_MazeDto)
                {
                    context.Attach(examSetIndex);
                }
            }
        }
    }
}