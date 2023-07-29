using Gitignorerer.API;
using Gitignorerer.IO;
using Gitignorerer.Utils;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

namespace Gitignorerer
{
    [Command(Name = "Gitignorerer", Description = "A tool to make Gitignore files easily.")]
    [HelpOption]
    public class Program
    {
        public static void Main(string[] args)
        {

            var services = new ServiceCollection()
                .AddSingleton<IGitignorererApplication, GitignorererApplication>()
                .AddSingleton(PhysicalConsole.Singleton)
                .AddSingleton<IConsoleWrapper, ConsoleWrapper>()
                .AddHttpClient()
                .AddSingleton<IGitignoreClient, GithubGitignoreClient>()
                .AddSingleton<IGitignoreWriter, GitignoreWriter>()
                .BuildServiceProvider();

            var app = new CommandLineApplication<Program>();

            app.Conventions
                .UseDefaultConventions()
                .UseConstructorInjection(services);

            app.Execute(args);
        }

        [Argument(0, Name = "Ignore files", Description = "Ignore file names to add to a .gitignore file")]
        public HashSet<string> IgnoreFileNames { get; }

        private readonly IGitignorererApplication _gitignorererApplication;

        public Program(IGitignorererApplication gitignorererApplication)
        {
            _gitignorererApplication = gitignorererApplication;
        }

        private async Task OnExecuteAsync()
        {
            await _gitignorererApplication.Run(IgnoreFileNames);
        }
    }
}
