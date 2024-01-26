using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Theseus.Infrastructure.DbContexts;

namespace Theseus.WPF.Code.HostBuilders
{
    /// <summary>
    /// The <c>AddDbContextHostBuilderExtensions</c> class registers DbContexts as singleton services.
    /// </summary>
    public static class AddDbContextHostBuilderExtensions
    {
        public static IHostBuilder AddDbContext(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                string connectionString = context.Configuration.GetConnectionString("TheseusDb");
                services.AddSingleton<DbContextOptions>(new DbContextOptionsBuilder().UseSqlServer(connectionString).Options);
                services.AddSingleton<TheseusDbContextFactory>();
            });

            return hostBuilder;
        }
    }
}