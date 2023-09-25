using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Theseus.Code.DbContexts
{
    public class TheseusDesignTimeDbContextFactory : IDesignTimeDbContextFactory<TheseusDbContext>
    {
        public TheseusDbContext CreateDbContext(string[] args)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\VisualStudioRepositories\\Theseus\\Theseus\\TheseusDatabase.mdf;Integrated Security=True").Options;

            return new TheseusDbContext(options);
        }
    }
}
