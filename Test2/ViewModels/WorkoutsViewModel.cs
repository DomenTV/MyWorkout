using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test2.Models;


namespace Test2.ViewModels
{
    internal class WorkoutsViewModel
    {
        private INavigation _navigation;
        public string equipment { get; set; }
        public string workout_type { get; set; }
        public string body_part { get; set; }
        public string difficulty { get; set; }
        public string number_of_exercises { get; set; }
        public string injury { get; set; }
        public Command StartButton { get; }
        //public int width { get; set; }

        public WorkoutsViewModel(INavigation navigation)
        {

            this._navigation = navigation;
            equipment = "No gear";
            workout_type = "Strengh";
            body_part = "Upper body";
            difficulty = "Level III - Normal";
            number_of_exercises = "2";
            injury = "None";
            StartButton = new Command(StartButtonTappedAsync);
            //width = Convert.ToInt32(Math.Round(DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density * 0.9));

        }

        private async void StartButtonTappedAsync(object obj)
        {
            //Global.oftype = workout_type;
            //Global.bodyPart = body_part;
            try
            {

                // Replace YOUR_PASSWORD with the actual password
                string connectionString = "server=192.168.*.*;user=*;database=myworkout1;port=*;password=*";

                // Use the connection string to create a new MySqlConnection object
                await using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a new MySqlCommand object
                    MySqlCommand cmd = connection.CreateCommand();
                    // Convert the difficulty string to an integer value
                    int level;
                    if (difficulty.Contains("Light"))
                        level = 1;
                    else if (difficulty.Contains("Easy"))
                        level = 2;
                    else if (difficulty.Contains("Normal"))
                        level = 3;
                    else if (difficulty.Contains("Hard"))
                        level = 4;
                    else if (difficulty.Contains("Advanced"))
                        level = 5;
                    else
                        level = 0;
                    // Set the SQL query
                    cmd.CommandText = $"SELECT * FROM Exercises WHERE equpment='{equipment}' AND oftype='{workout_type}' AND bodyPart LIKE '%{body_part}%' AND level={level} ORDER BY RAND() LIMIT 100";

                    // Set the parameters
                    //cmd.Parameters.AddWithValue("@equipment", equpment);
                    //cmd.Parameters.AddWithValue("@workout_type", workout_type);
                    //cmd.Parameters.AddWithValue("@body_part", "%" + body_part + "%");



                    //cmd.Parameters.AddWithValue("@difficulty", level);
                    //cmd.Parameters.AddWithValue("@injury", injury);
                    //cmd.Parameters.AddWithValue("@number_of_exercises", number_of_exercises);

                    // Execute the query and get the result
                    //await App.Current.MainPage.DisplayAlert("Query", cmd.CommandText, "OK");
                    MySqlDataReader reader = cmd.ExecuteReader();

                    // Create a list to store the records
                    //List<Exercise> exercises = new List<Exercise>();
                    Global.exercises.Clear();
                    // Iterate through the result set and create Exercise objects for each record
                    while (reader.Read())
                    {
                        Exercise exercise = new Exercise
                        {
                            idExercises = reader.GetInt32("idExercises"),
                            name = reader.GetString("name"),
                            repsandsets = reader.GetString("repsandsets"),
                            texttip = reader.GetString("texttip"),
                            exerciseImage = reader.GetString("exerciseImage"),
                            muscleImage = reader.GetString("muscleImage"),
                            //equpment = reader.GetString("equpment"),
                            oftype = reader.GetString("oftype"),
                            //bodyParts = reader.GetString("bodyParts"),
                            level = reader.GetInt32("level"),
                            //bodyPart = reader.GetString("bodyPart")
                        };

                        // Add the Exercise object to the list
                        Global.exercises.Add(exercise);
                    }

                    // Close the reader
                    reader.Close();

                }
                if (Global.exercises.Count == 0)
                {
                    throw new Exception("No exercisese were found");
                }
                //await App.Current.MainPage.DisplayAlert("Wrong login information!", Global.exercises.Count.ToString(), "OK");
            }
            catch (Exception ee)
            {
                await App.Current.MainPage.DisplayAlert("Query", ee.Message, "OK");
            }
            //-------------------------------------------------------ž
            await this._navigation.PushAsync(new WorkoutsDisplay());
        }
    }
}

