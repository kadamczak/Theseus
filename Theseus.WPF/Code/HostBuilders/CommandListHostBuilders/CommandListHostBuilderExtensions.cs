using Microsoft.Extensions.Hosting;

namespace Theseus.WPF.Code.HostBuilders.CommandListHostBuilders
{
    public static class CommandListHostBuilderExtensions
    {
        public static IHostBuilder AddCommandLists(this IHostBuilder hostBuilder)
        {
            hostBuilder.AddCommandListFactories()
                        .AddGranterFactories()
                        .AddCommandGranters()
                        .AddInfoGranters();

            return hostBuilder;
        }
    }
}