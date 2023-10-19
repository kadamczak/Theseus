using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Theseus.Domain.CommandInterfaces;
using Theseus.Domain.QueryInterfaces;
using Theseus.Infrastructure.Commands;
using Theseus.Infrastructure.Queries;

namespace Theseus.WPF.Code.HostBuilders
{
    public static class AddCommandHostBuilderExtensions
    {
        public static IHostBuilder AddCommands(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<IGetAllMazesWithSolutionQuery, GetAllMazesWithSolutionQuery>();
                services.AddSingleton<ICreateOrUpdateMazeWithSolutionCommand, CreateOrUpdateMazeWithSolutionCommand>();
            });

            return hostBuilder;
        }
    }
}
