using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.MazeRelated.MazeCreators;
using Theseus.Domain.Models.MazeRelated.MazeGenerators;
using Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators;
using Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators.HelperClasses;

namespace Theseus.WPF.Code.HostBuilders
{
    /// <summary>
    /// The <c>AddFactoriesHostBuilderExtensions</c> class registers domain factories as singleton services.
    /// </summary>
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
                services.AddSingleton<ExamSetCreator>();
            });

            return hostBuilder;
        }
    }
}