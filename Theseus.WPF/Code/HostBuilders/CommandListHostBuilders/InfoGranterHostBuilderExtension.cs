using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList.Info.Implementations;
using Theseus.WPF.Code.ViewModels.GroupViewModels.GroupCommandList.Info.Implementations;
using Theseus.WPF.Code.ViewModels.MazeViewModels.MazeCommandList.Info.Implementations;

namespace Theseus.WPF.Code.HostBuilders.CommandListHostBuilders
{
    public static class InfoGranterHostBuilderExtension
    {
        public static IHostBuilder AddInfoGranters(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddTransient<EmptyExamSetInfoGranter>();
                services.AddTransient<GeneralExamSetInfoGranter>();
                services.AddTransient<OwnerExamSetInfoGranter>();

                services.AddTransient<EmptyMazeInfoGranter>();
                services.AddTransient<GeneralMazeInfoGranter>();

                services.AddTransient<EmptyGroupInfoGranter>();
                services.AddTransient<GeneralGroupInfoGranter>();
            });

            return hostBuilder;
        }
    }
}
