using Gitignorerer;
using Gitignorerer.API;
using Gitignorerer.IO;
using Gitignorerer.Parsers;
using Gitignorerer.Utils;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection()
    .AddSingleton<IGitignorererApplication, GitignorererApplication>()
    .AddSingleton(PhysicalConsole.Singleton)
    .AddSingleton<IConsoleWrapper, ConsoleWrapper>()
    .AddHttpClient()
    .AddSingleton<IGitignoreClient, GithubGitignoreClient>()
    .AddSingleton<IGitignoreWriter, GitignoreWriter>()
    .BuildServiceProvider();

var app = new CommandLineApplication() 
{ 
    Name = "gitignorerer",
    Description = "A tool to make Gitignore files easily.",
};
app.ValueParsers.Add(new StringToHashSetParser());
app.HelpOption();
var ignoreFiles = app.Argument<HashSet<string>>("Ignore files", "Ignore file names to add to a .gitignore file", multipleValues: true).IsRequired();
app.Conventions
    .UseDefaultConventions()
    .UseConstructorInjection(services);

app.OnExecuteAsync(async cancellationtoken =>
{
    await services.GetRequiredService<IGitignorererApplication>().Run(ignoreFiles.ParsedValue);
});

return await app.ExecuteAsync(args);
