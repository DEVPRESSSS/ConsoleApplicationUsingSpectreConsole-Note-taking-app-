using Spectre.Console;
using System.Data.SqlClient;
using System.Net.Http.Headers;

namespace SpectreConsoleAppProject
{
    internal class Program
    {
        public static List<string> tasks = new List<string>();

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

            Prompt();







        }

        public enum Emojies{

            Add = 1, 
            View= 2,
            Update= 3,
            Close =4


        }
        private static void Prompt()
        {


            string add = Emoji.Known.Plus.ToString();
            string magnify = Emoji.Known.MagnifyingGlassTiltedLeft.ToString();
            string close = Emoji.Known.ClosedBook.ToString();
            string update= Emoji.Known.Pencil.ToString();
            string delete= Emoji.Known.Wastebasket.ToString();
            while (true)
            {

                var addTask = AnsiConsole.Prompt(
                  new SelectionPrompt<string>()
                     .Title("[yellow]Hi jerald, What would you like to do?[/]")
                     .AddChoices($"{add}Add Task", $"{magnify}View Tasks", $"{update}Update Tasks", $"{delete} Delete Tasks", $"{close}Exit"));


                if (addTask == $"{add}Add Task")
                {
                    // Simulate the Add Task action
                    //  AnsiConsole.Clear();
                    Addtask();



                }
                else if (addTask == $"{magnify}View Tasks")
                {
                    // Simulate View Tasks action

                    AnsiConsole.Clear();
                    CRUD.ViewAllTask();
                }

                else if (addTask == $"{update} Update Tasks")
                {
                    // Simulate View Tasks action

                   AnsiConsole.Clear();
                   CRUD.ViewAllTask();


                  // Thread.Sleep(100);
                   UpdateTaskStatus();
                }
                else if (addTask == $"{delete} Delete Tasks")
                {
                    // Simulate View Tasks action

                    AnsiConsole.Clear();
                    CRUD.ViewAllTask();


                    // Thread.Sleep(100);
                    DeleteTask();
                }
                else if (addTask == $"{close}Exit")
                {

                    var confirmation = AnsiConsole.Prompt(
                        new TextPrompt<bool>("Are you sure you want to exit?")
                            .AddChoice(true)
                            .AddChoice(false)
                            .DefaultValue(true)
                            .WithConverter(choice => choice ? "y" : "n"));

                    // Echo the confirmation back to the terminal
                    Console.WriteLine(confirmation ? "Confirmed" : "Declined");

                    if (confirmation == true)
                    {


                        AnsiConsole.Progress()
                             .Start(ctx =>
                             {
                                 // Define tasks
                                 var task1 = ctx.AddTask("[yellow]Closing console app[/]");

                                 while (!ctx.IsFinished)
                                 {
                                     task1.Increment(1.5);
                                     System.Threading.Thread.Sleep(20);

                                 }
                             });
                          AnsiConsole.MarkupLine("[bold red]Closing the application...[/]");

                        AnsiConsole.Write(
                        new FigletText("Thank you for using my Todo App \n \n")
                            .Centered()
                            .Color(Color.Yellow));


                        Environment.Exit(0);

                    }

                
                }



            }

        }

        private static void Addtask()
        {
            var curious = Emoji.Known.WhiteQuestionMark.ToString();
            var taskname = AnsiConsole.Prompt(
                new TextPrompt<string> ($"What task you want to accomplised today {curious}:")
                );


            //AnsiConsole.WriteLine(taskname);

            // tasks.Add(taskname);

            var insert = new CRUD();

            insert.TaskName= taskname;

            insert.Insert();


            AnsiConsole.MarkupLine("[bold yellow]Task added successfully![/]");
            CRUD.ViewAllTask();


        }

        private static void UpdateTaskStatus()
        {
           
            var updateStatus = AnsiConsole.Prompt(
            new TextPrompt<string>($"Update status of your task, please enter a valid taskID:")
            );



            var obj= new CRUD();

            obj.TaskID = Convert.ToInt32(updateStatus);

            obj.UpdateStatus(obj.TaskID);
            AnsiConsole.Clear();
            CRUD.ViewAllTask();
        }
        private static void DeleteTask()
        {

            var delete = AnsiConsole.Prompt(
            new TextPrompt<string>($"Delete your task, please enter a valid taskID:")
            );



            var obj = new CRUD();

            obj.TaskID = Convert.ToInt32(delete);

            obj.DeleteTask(obj.TaskID);
            AnsiConsole.Clear();
            CRUD.ViewAllTask();
        }


    }
}
