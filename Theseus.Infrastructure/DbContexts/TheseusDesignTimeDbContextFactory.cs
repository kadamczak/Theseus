using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Theseus.Infrastructure.DbContexts
{
    public class TheseusDesignTimeDbContextFactory : IDesignTimeDbContextFactory<TheseusDbContext>
    {
        public TheseusDbContext CreateDbContext(string[] args)
        {
            return new TheseusDbContext(new DbContextOptionsBuilder().UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\VisualStudioRepositories\\TheseusMvvm\\Theseus\\Theseus.Infrastructure\\TheseusDb.mdf;Integrated Security=True").Options);
        }
    }
}