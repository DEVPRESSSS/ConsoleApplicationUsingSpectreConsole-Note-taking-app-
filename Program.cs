using Spectre.Console;

namespace SpectreConsoleAppProject
{
    internal class Program
    {
        static void Main(string[] args)
        {



            Body();
        }

        private static void Body()
        {

            //For better support of Unicode, to avoid unsupported unicodes
            Console.OutputEncoding = System.Text.Encoding.UTF8;


            //Welcome Message 
            AnsiConsole.Write(
                    new FigletText("Devpress_101 Note Taking App \n \n")
                        .Centered()
                        .Color(Color.Yellow));


            // Declare and emoji assign in hello variable
            var hello = Emoji.Known.FountainPen;


            // Create a rule with the emoji interploated
            var rule = new Rule($"[yellow] Your Notes{hello}[/]");


            //Print the rule in the console
            AnsiConsole.Write(rule);




            // Render the layout of the list using Table in SpectreConsole
        }
    }
}
