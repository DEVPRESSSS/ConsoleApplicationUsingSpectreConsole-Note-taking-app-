using Spectre.Console;
using System.Net.Http.Headers;

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

            Console.WriteLine();


            //Prompt the user
            var addTask = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
               .Title("[green]Hi jerald, What would you like to do?[/]")
               .AddChoices("Add Task", "View Tasks", "Exit"));

            if (addTask == "Add Task")
            {
                // Simulate the Add Task action
                Addtask();
            }
            else if (addTask == "View Tasks")
            {
                // Simulate View Tasks action
                AnsiConsole.MarkupLine("[bold yellow]Here are your tasks...[/]");
            }
            else if (addTask == "Exit ")
            {
                AnsiConsole.MarkupLine("[bold red]Exiting...[/]");
            }


            // Declare and emoji assign in hello variable
            var hello = Emoji.Known.FountainPen;


            // Create a rule with the emoji interploated
            var rule = new Rule($"[yellow] Your task today{hello}[/]");


            //Print the rule in the console
            AnsiConsole.Write(rule);


            // Render the layout of the list using Table in SpectreConsole

            var check_emojie = Emoji.Known.CheckMarkButton;
            var notes_table = new Table();


            //Styles in table
            notes_table.Border(TableBorder.Rounded);


            notes_table.AddColumn("TaskID");
            notes_table.AddColumn("Task");
            notes_table.AddColumn(new TableColumn("Status").Centered());


            notes_table.AddRow($"Task01", "Forgot pasword", "Done" + check_emojie);
            notes_table.AddRow($"Task02", "Bug in change pass", "Done" + check_emojie);
            notes_table.AddRow($"Task03", "Bug in login", "Done" + check_emojie);
            notes_table.AddRow($"Tasl04", "Bug in dashboard", "Done" + check_emojie);

            //notes_table.AddRow(new Markup("");

            notes_table.Width(1000);
            notes_table.Expand();
            AnsiConsole.Write(notes_table);
        }


        private static void Addtask()
        {

            var taskname = AnsiConsole.Prompt(
                new TextPrompt<string> ("What task you want to accomplised today?")
                );


            AnsiConsole.WriteLine(taskname);

            AnsiConsole.MarkupLine("[bold green]Task added successfully![/]");


        }
    }
}
