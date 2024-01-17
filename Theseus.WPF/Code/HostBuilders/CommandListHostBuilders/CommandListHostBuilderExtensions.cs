using Microsoft.Extensions.Hosting;

namespace Theseus.WPF.Code.HostBuilders.CommandListHostBuilders
{
    /// <summary>
    /// The <c>CommandListHostBuilderExtensions</c> class calls command list setup methods.
    /// </summary>
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