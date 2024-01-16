using Microsoft.Extensions.Hosting;

namespace Theseus.WPF.Code.HostBuilders.ViewModelHostBuilders
{
    public static class AddViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder)
        {
            hostBuilder.AddSingletonViewModels()
                        .AddNavbarViewModels()
                        .AddMazeViewModels()
                        .AddDataViewModels()
                        .AddExamSetViewModels()
                        .AddExamViewModels()
                        .AddAuthenticationViewModels()
                        .AddGroupViewModels();

            return hostBuilder;
        }
    }
}