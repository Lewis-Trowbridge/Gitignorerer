using McMaster.Extensions.CommandLineUtils;

namespace Gitignorerer
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var app = new CommandLineApplication();

            app.HelpOption();

            var ignoreFileNames = app.Argument("Ignore files", "Ignore file names to add to a .gitignore file", multipleValues: true);

            app.OnExecute(() =>
            {
                GitignorererApplication.Run(ignoreFileNames);
            });

            app.Execute(args);
        }
    }
}
