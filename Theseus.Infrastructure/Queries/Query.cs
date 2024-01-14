using AutoMapper;
using Theseus.Infrastructure.DbContexts;

namespace Theseus.Infrastructure.Queries
{
    /// <summary>
    /// Abstract class defining usage of <c>TheseusDbContextFactory</c> and <c>IMapper</c>.
    /// </summary>
    public abstract class Query
    {
        protected TheseusDbContextFactory DbContextFactory { get; }
        protected IMapper Mapper { get; }

        public Query(TheseusDbContextFactory dbContextFactory, IMapper mapper)
        {
            DbContextFactory = dbContextFactory;
            Mapper = mapper;
        }
    }
}
