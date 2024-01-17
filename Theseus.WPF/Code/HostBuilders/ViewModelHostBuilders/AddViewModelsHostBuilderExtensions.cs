using Microsoft.Extensions.Hosting;

namespace Theseus.WPF.Code.HostBuilders.ViewModelHostBuilders
{
    /// <summary>
    /// The <c>AddViewModelsHostBuilderExtensions</c> class calls view model setup methods. 
    /// </summary>
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