using AutoMapper;
using Theseus.Infrastructure.DbContexts;

namespace Theseus.Infrastructure.Queries
{
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
