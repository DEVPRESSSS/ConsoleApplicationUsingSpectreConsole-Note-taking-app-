using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectreConsoleAppProject
{
    public class CRUD
    {

        public int TaskID { get; set; }

        public string? TaskName { get; set; }
        public string Status = "Pending...";




        private static string connectionString = "Server=REJIE\\SQLEXPRESS;Database=TodoAppConsole;Trusted_Connection=True;TrustServerCertificate=True;";


        public void Insert()
        {
            string query = "INSERT INTO Tasks (TaskName) VALUES (@TaskName)";


            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                conn.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query,conn))
                {
                    sqlCommand.Parameters.AddWithValue("@TaskName", TaskName);
                   // sqlCommand.Parameters.AddWithValue("@TaskName", Status);


                    sqlCommand.ExecuteNonQuery();
                    Console.Beep();
                };


            }

        }

        public static void ViewAllTask()
        {

            // Declare and emoji assign in hello variable
            var pen = Emoji.Known.FountainPen;
            var empty = Emoji.Known.CryingCat;

            // Create a rule with the emoji interploated
            var rule = new Rule($"[yellow] Your task today{pen}[/]");


            //Empty rule

            var rule2 = new Rule($"[yellow] Your task today is empty {empty}[/]");

            /*if (tasks.Count > 0)
            {

                //Print the rule in the console
                AnsiConsole.Write(rule);

            }
            else
            {

                AnsiConsole.Write(rule2);


            }*/

            var pending = Emoji.Known.Snail;


            var emojidone = Emoji.Known.CheckMarkButton;



            var notes_table = new Table();






            //Styles in table
            notes_table.Border(TableBorder.Rounded);


            notes_table.AddColumn("TaskID");
            notes_table.AddColumn("Task");
            notes_table.AddColumn(new TableColumn("Status").Centered());


            /* int increment = 0;
             foreach (var task in tasks)
             {

                 increment++;
                 var taskID = "Task" + increment;


                 notes_table.AddRow($"{taskID}", $"{task}", "Pending.." + check_emojie);





             }
            */
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Tasks";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string taskID = $"{reader["TaskID"]}";
                    string taskName = $"{reader["TaskName"]}";
                    string? status = reader["Status"].ToString();

                    string emoji = status == "Finished" ? emojidone : pending;

                    notes_table.AddRow(taskID, taskName, $"{status} {emoji}");



                }
            }

            notes_table.Width(1000);
            notes_table.Expand();
            AnsiConsole.Write(notes_table);
        }

        public void UpdateStatus( int TaskId)
        {
            string done = "Finished";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "UPDATE Tasks SET Status = @Status WHERE TaskID = @TaskID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Status", done);

                    cmd.Parameters.AddWithValue("@TaskID", TaskId);

                    // Execute the update command
                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Check if any rows were affected
                    if (rowsAffected > 0)
                    {

                        Console.Beep();
                    }
               
                }
            }
        }



        public void DeleteTask(int id)
        {
            try
            {

                if (id > 0)
                {

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();

                        string query = "DELETE FROM Tasks WHERE TaskID = @TaskID";

                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            // Add parameters for the SQL query

                            cmd.Parameters.AddWithValue("@TaskID", id);

                            // Execute the update command
                            int rowsAffected = cmd.ExecuteNonQuery();

                            // Check if any rows were affected
                            if (rowsAffected > 0) { 
                                    

                               
                                Console.Beep();
                                AnsiConsole.MarkupLine($"[bold yellow]Task {id} deleted successfully![/]");


                            }

                        }
                    }

                }
       



            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }


        }


    }
}
