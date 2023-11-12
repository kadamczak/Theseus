using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Theseus.Domain.CommandInterfaces;
using Theseus.Infrastructure.Commands;

namespace Theseus.WPF.Code.HostBuilders
{
    public static class AddCommandsHostBuilderExtensions
    {
        public static IHostBuilder AddCommands(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<ICreateOrUpdateMazeWithSolutionCommand, CreateOrUpdateMazeWithSolutionCommand>();
                services.AddSingleton<ICreateExamSetCommand, CreateExamSetCommand>();
                services.AddSingleton<ICreateStaffMemberCommand, CreateStaffMemberCommand>();
                services.AddSingleton<ICreatePatientCommand, CreatePatientCommand>();
            });

            return hostBuilder;
        }
    }
}