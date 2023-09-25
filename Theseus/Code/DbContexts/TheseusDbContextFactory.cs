using Microsoft.EntityFrameworkCore;

namespace Theseus.Code.DbContexts
{
    public class TheseusDbContextFactory
    {
        private readonly string _connectionString;

        public TheseusDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public TheseusDbContext CreateDbContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlServer(_connectionString).Options;

            return new TheseusDbContext(options);
        }

    }
}
