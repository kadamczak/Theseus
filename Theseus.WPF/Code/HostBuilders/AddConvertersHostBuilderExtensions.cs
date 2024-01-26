using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Theseus.Infrastructure.Dtos.Converters.MazeConverters;

namespace Theseus.WPF.Code.HostBuilders
{
    /// <summary>
    /// The <c>AddConvertersHostBuilderExtensions</c> class registers Converters as singleton services.
    /// </summary>
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