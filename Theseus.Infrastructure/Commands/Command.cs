using AutoMapper;
using Theseus.Infrastructure.DbContexts;

namespace Theseus.Infrastructure.Commands
{
    /// <summary>
    /// Abstract class defining usage of <c>TheseusDbContextFactory</c> and <c>IMapper</c>.
    /// </summary>
    public abstract class Command
    {
        protected TheseusDbContextFactory DbContextFactory { get; }
        protected IMapper Mapper { get; }

        public Command(TheseusDbContextFactory dbContextFactory, IMapper mapper)
        {
            DbContextFactory = dbContextFactory;
            Mapper = mapper;
        }
    }
}
