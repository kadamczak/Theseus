using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Theseus.Infrastructure.Dtos.Converters;

namespace Theseus.WPF.Code.HostBuilders
{
    public static class AddConvertersHostBuilderExtensions
    {
        public static IHostBuilder AddConverters(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<MazeDtoToMazeWithSolutionConverter>();
                services.AddSingleton<MazeWithSolutionToMazeDtoConverter>();
            });

            return hostBuilder;
        }
    }
}
