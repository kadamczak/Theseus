using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Theseus.Domain.Models.MazeRelated.MazeCreators;
using Theseus.Domain.Models.MazeRelated.MazeGenerators;
using Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators;
using Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators.HelperClasses;

namespace Theseus.WPF.Code.HostBuilders
{
    public static class AddFactoriesHostBuilderExtensions
    {
        public static IHostBuilder AddFactories(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<DistanceGridFactory>();
                services.AddSingleton<MazeStructureGeneratorFactory>();
                services.AddSingleton<MazeSolutionGeneratorFactory>();
                services.AddSingleton<MazeCreator>();
            });

            return hostBuilder;
        }
    }
}