using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;
using LiteDB;
using NeoSmart.Utils;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
//using static Android.Icu.Text.CaseMap;


namespace Test2.ViewModels
{
    public class Exercise : INotifyPropertyChanged
    {
        public int idExercises { get; set; }
        public string name { get; set; }
        public string repsandsets { get; set; }
        public string texttip { get; set; }
        public string exerciseImage { get; set; }
        public MemoryStream exerciseImage2 { get; set; }
        public ImageSource exerciseImage3 { get; set; }
        public string muscleImage { get; set; }
        public MemoryStream muscleImage2 { get; set; }
        public ImageSource muscleImage3 { get; set; }
        public ImageSource muscleTemplate { get; set; }
        public string equipment { get; set; }
        public string oftype { get; set; }
        public int level { get; set; }
        public bool upperBody { get; set; }
        public bool coreBody { get; set; }
        public bool lowerBody { get; set; }
        public bool neck { get; set; }
        public bool back { get; set; }
        public bool chest { get; set; }
        public bool arm { get; set; }
        public bool wrist { get; set; }
        public bool abdominalArea { get; set; }
        public bool leg { get; set; }
        public bool ankle { get; set; }
        private bool _isInfoVisible;
        public bool IsInfoVisible
        {
            get => _isInfoVisible;
            set
            {
                _isInfoVisible = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    internal partial class MyWorkoutViewModel : ObservableObject
    {

        private INavigation _navigation;
        public string equipment { get; set; }
        public string workout_type { get; set; }
        public string body_part { get; set; }
        public string difficulty { get; set; }
        public string number_of_exercises { get; set; }
        public string dailyFeel { get; set; }
        public string injury { get; set; }
        public string qa { get; set; }
        public bool neckInjury { get; set; }
        public bool backInjury { get; set; }
        public bool chestInjury { get; set; }
        public bool armInjury { get; set; }
        public bool wristInjury { get; set; }
        public bool abdominalInjury { get; set; }
        public bool legInjury { get; set; }
        public bool ankleInjury { get; set; }
        public Command StartButton { get; }
        public Command ProfileButton { get; }
        public Command ExercisesButton { get; }
        public Command qaButton { get; }
        //public int width { get; set; }
        public bool recommendations { get; set; }
        [ObservableProperty]
        private bool isLoadingVisible;


        public List<string> SelectedInjuries { get; set; } = new List<string>();

        
        public MyWorkoutViewModel(INavigation navigation)
        {

            this._navigation = navigation;
            //IsLoadingVisible = false;
            LoadPastWorkouts();
            if (Global.pastWorkouts.Count > 0)
            {
                qa = "FAQ";
                equipment = Global.pastWorkouts[0].equipment;
                workout_type = Global.pastWorkouts[0].oftype;
                body_part = Global.pastWorkouts[0].bodyPart;
                recommendations = Convert.ToBoolean(Global.pastWorkouts[0].recommendations);
                switch (Global.pastWorkouts[0].level)
                {
                    case 1:
                        difficulty = "Level I - Light";
                        break;
                    case 2:
                        difficulty = "Level II - Easy";
                        break;
                    case 3:
                        difficulty = "Level III - Normal";
                        break;
                    case 4:
                        difficulty = "Level IV - Hard";
                        break;
                    case 5:
                        difficulty = "Level V - Advanced";
                        break;
                    default:
                        difficulty = "Level III - Normal";
                        break;
                }
                number_of_exercises = Global.pastWorkouts[0].numExercises.ToString();
                neckInjury = Convert.ToBoolean(Global.pastWorkouts[0].neck);
                backInjury = Convert.ToBoolean(Global.pastWorkouts[0].back);
                chestInjury = Convert.ToBoolean(Global.pastWorkouts[0].chest);
                armInjury = Convert.ToBoolean(Global.pastWorkouts[0].arm);
                wristInjury = Convert.ToBoolean(Global.pastWorkouts[0].wrist);
                abdominalInjury = Convert.ToBoolean(Global.pastWorkouts[0].abdominalArea);
                legInjury = Convert.ToBoolean(Global.pastWorkouts[0].leg);
                ankleInjury = Convert.ToBoolean(Global.pastWorkouts[0].ankle);
                dailyFeel = "Average";
            }
            else
            {
                qa = "Q&A";
                equipment = "No gear";
                workout_type = "Strength";
                body_part = "Upper body";
                difficulty = "Level III - Normal";
                number_of_exercises = "3";
                recommendations = true;
                dailyFeel = "Average";
            }
            
            StartButton = new Command(StartButtonTappedAsync);
            ProfileButton = new Command(ProfileButtonTappedAsync);
            ExercisesButton = new Command(ExercisesButtonTappedAsync);
            qaButton = new Command(qaButtonTappedAsync);
            //width = Convert.ToInt32(Math.Round(DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density * 0.9));

        }
        private async void ExercisesButtonTappedAsync(object obj)
        {
            await this._navigation.PushAsync(new MyWorkout());
        }
        private async void qaButtonTappedAsync(object obj)
        {
            //await this._navigation.PushAsync(new QuestionsAnswers());
            IsLoadingVisible = true;
            await this._navigation.PushAsync(new FAQ());
            IsLoadingVisible = false;
        }
        private async static void LoadPastWorkouts()
        {
            int usrID = 0;
            string _connectionString = "server=192.168.*.*;user=*;database=myworkout1;port=*;password=*";
            //----------GET USER ID-----------------------------------------------------------------------------------------------------------
            // Use the connection string to create a new MySqlConnection object
            await using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {

                connection.Open();

                MySqlCommand cmd = connection.CreateCommand();

                // Set the SQL query
                cmd.CommandText = $"SELECT idUsers FROM users WHERE email='{Global.globalEmail}' LIMIT 1;";

                MySqlDataReader reader = cmd.ExecuteReader();
                //await App.Current.MainPage.DisplayAlert("How to:", cmd.CommandText, "OK");


                while (reader.Read())
                {
                    //usrID = int.Parse(reader.GetString("idUsers"));
                    usrID = reader.GetInt32(reader.GetOrdinal("idUsers"));


                }
                //await App.Current.MainPage.DisplayAlert("MADE IT", "MADE IT!", "OK");
                reader.Close();

                //-----------------------------------------------------------------------------------------------------------------------
                //cmd.CommandText = $"SELECT idPastWorkouts, date, duration, oftype, bodypart FROM pastWorkouts WHERE Users_idUsers = {usrID} ORDER BY date DESC LIMIT 100;";
                cmd.CommandText = $"SELECT idPastWorkouts, date, duration, intensity, equipment, oftype, bodypart, level, numberOfExercises, neck, back, chest, arm, wrist, abdominalArea, leg, ankle, recommendations FROM pastWorkouts WHERE Users_idUsers = {usrID} ORDER BY idPastWorkouts DESC LIMIT 20;";
                MySqlDataReader reader2 = cmd.ExecuteReader();
                //await App.Current.MainPage.DisplayAlert("How to:", cmd.CommandText, "OK");

                Global.pastWorkouts.Clear();

                while (reader2.Read())
                {
                    //await App.Current.MainPage.DisplayAlert("How to:", reader2.GetString("level"), "OK");
                    //PastWorkouts workouts = new PastWorkouts                                                          FOR MYSQL.DATA ver. 8.0.31  !!!!!
                    //{
                    //    pastWorkoutID = reader2.GetInt32("idPastWorkouts"),
                    //    date = reader2.GetString("date").Replace(" 00:00:00", ""),
                    //    duration = reader2.GetString("duration"),
                    //    intensity = Int32.Parse(reader2.GetString("intensity")),
                    //    equipment = reader2.GetString("equipment"),
                    //    oftype = reader2.GetString("oftype"),
                    //    bodyPart = reader2.GetString("bodyPart"),
                    //    level = Int32.Parse(reader2.GetString("level")),
                    //    numExercises = Int32.Parse(reader2.GetString("numberOfExercises")),
                    //    neck = Int32.Parse(reader2.GetString("neck")),
                    //    back = Int32.Parse(reader2.GetString("back")),
                    //    chest = Int32.Parse(reader2.GetString("chest")),
                    //    arm = Int32.Parse(reader2.GetString("arm")),
                    //    wrist = Int32.Parse(reader2.GetString("wrist")),
                    //    abdominalArea = Int32.Parse(reader2.GetString("abdominalArea")),
                    //    leg = Int32.Parse(reader2.GetString("leg")),
                    //    ankle = Int32.Parse(reader2.GetString("ankle")),
                    //    recommendations = Int32.Parse(reader2.GetString("recommendations"))
                    //};
                    PastWorkouts workouts = new PastWorkouts
                    {
                        pastWorkoutID = reader2.GetInt32("idPastWorkouts"),
                        date = reader2.GetDateTime("date").ToString("yyyy-MM-dd"), // Convert DateTime to formatted string
                        duration = reader2.GetString("duration"),
                        intensity = reader2.GetInt32("intensity"), // Use GetInt32 instead of parsing a string
                        equipment = reader2.GetString("equipment"),
                        oftype = reader2.GetString("oftype"),
                        bodyPart = reader2.GetString("bodyPart"),
                        level = reader2.GetInt32("level"),
                        numExercises = reader2.GetInt32("numberOfExercises"),
                        neck = reader2.GetInt32("neck"),
                        back = reader2.GetInt32("back"),
                        chest = reader2.GetInt32("chest"),
                        arm = reader2.GetInt32("arm"),
                        wrist = reader2.GetInt32("wrist"),
                        abdominalArea = reader2.GetInt32("abdominalArea"),
                        leg = reader2.GetInt32("leg"),
                        ankle = reader2.GetInt32("ankle"),
                        recommendations = reader2.GetInt32("recommendations")
                    };


                    // Add the Exercise object to the list
                    Global.pastWorkouts.Add(workouts);

                }
                //await App.Current.MainPage.DisplayAlert("How to:", Global.pastWorkouts.Count.ToString(), "OK");
                reader2.Close();
            }
           
        }
        private async void ProfileButtonTappedAsync(object obj)
        {
            IsLoadingVisible = true;
            await this._navigation.PushAsync(new User());
            IsLoadingVisible = false;
        }
        private async void StartButtonTappedAsync(object obj)
        {
            IsLoadingVisible = true;
            await Task.Delay(1);
            //await App.Current.MainPage.DisplayAlert("How to:", IsLoadingVisible.ToString(), "OK");
            if (recommendations == true)
            {
                Global.recommendations = 1;
            }
            else { Global.recommendations = 0; }
            Global.oftype = workout_type;
            Global.bodyPart = body_part;
            int level = 0;
            if (difficulty.Contains("Light")) level = 1;
            else if (difficulty.Contains("Easy")) level = 2;
            else if (difficulty.Contains("Normal")) level = 3;
            else if (difficulty.Contains("Hard")) level = 4;
            else if (difficulty.Contains("Advanced")) level = 5;
            else if (difficulty.Contains("Extreme")) level = 6;

            // --------------- ------------------ ------------------- Daily feel adjustment logic -------------- ----------------- ------------------- ---------------

            if (recommendations == true ) //if recommendations are turned on / wanted by user
            {
                //var abcd = await App.Current.MainPage.DisplayAlert("Test", "Do you want help?", "yes", "no");
                //await App.Current.MainPage.DisplayAlert("What u said", "You said: " + abcd, "ok");
                try
                {
                    DateTime threeWeeksAgo = DateTime.Now.Date.AddDays(-21);
                    //List<PastWorkouts> recentWorkoutsLoop = new List<PastWorkouts>();
                    List<PastWorkouts> recentWorkoutsLoopSorted = new List<PastWorkouts>();
                    foreach (var workout in Global.pastWorkouts)                        //Get workouts that are up to 3 weeks old
                    {
                        if (DateTime.TryParse(workout.date, out DateTime workoutDate) && workoutDate >= threeWeeksAgo)
                        {
                            //recentWorkoutsLoop.Add(workout);
                            if (workout.oftype == workout_type && workout.bodyPart == body_part)        //Get all compatible workouts (same selection parameters)
                            {
                                recentWorkoutsLoopSorted.Add(workout);
                            }
                        }
                    }
                    if (recentWorkoutsLoopSorted.Count < 3)
                    {
                        throw new Exception("Not enough past compatible workouts to adjust");
                    }

                    double averageIntensity = 0, averageLevel = 0;        //Average intensity (how hard the executed workout was) & average level
                    int totalIntensity = 0, totalLevel = 0;
                    foreach (var workout in recentWorkoutsLoopSorted)
                    {
                        totalIntensity += workout.intensity;
                        totalLevel += workout.level;
                    }
                    averageIntensity = Math.Round((double)totalIntensity / recentWorkoutsLoopSorted.Count, 2);
                    averageLevel = Math.Round((double)totalLevel / recentWorkoutsLoopSorted.Count, 0);
                    string currentLevel = "";
                    if ((int)averageLevel == level)
                    {
                        currentLevel = "Normal level";
                    }
                    else if ((int)averageLevel > level)
                    {
                        currentLevel = "Already lowered level";
                    }
                    else if ((int)averageLevel < level)
                    {
                        currentLevel = "Already higher level";
                    }
                    //await App.Current.MainPage.DisplayAlert("The stuff:", "Level: " + averageLevel.ToString() + "\n\nIntensity: " + averageIntensity.ToString(), "ok");

                    if (dailyFeel == "Average") // DF = 3   We want intensity between: (3 ; 3,5)
                    {
                        if (3.0 < averageIntensity && averageIntensity < 3.5) //    Intensity is OK; we want average level
                        {
                            if (currentLevel == "Normal level") 
                            {
                                ;
                            }
                            else if (currentLevel == "Already lowered level")   // We want average level but level is lower so we must increase level
                            {
                                bool descission = await App.Current.MainPage.DisplayAlert("Recommendation to increase the difficulty", "Based on your recent performance, we suggest increasing the difficulty of this workout to help you progress. \nGradual workload adjustments are essential for continuous improvement. \nWould you like to proceed with a higher difficulty level?", "Yes", "No");
                                if (descission)
                                {
                                    level++;
                                }
                            }
                            else if (currentLevel == "Already higher level")    // We want average level but level is higher so we must lower level
                            {
                                bool descission = await App.Current.MainPage.DisplayAlert("Recommendation to decrease the difficulty", "Based on your recent performance, we suggest decreasing the difficulty of this workout to help prevent overtraining and ensure optimal recovery. \n\nAdjusting the workload when needed is essential for long-term progress and avoiding burnout. \nThat said, if you're feeling up for the challenge, best of luck, give it your all and do your best!\n\nWould you like to lower the difficulty level?", "Yes", "No");
                                if (descission)
                                {
                                    level--;
                                }
                            }
                        }
                        else if (3.5 <= averageIntensity)   //    Intensity is TOO HIGH; we want to lower level
                        {
                            if (currentLevel == "Normal level")
                            {
                                bool descission = await App.Current.MainPage.DisplayAlert("Recommendation to decrease the difficulty", "Based on your recent performance, we suggest decreasing the difficulty of this workout to help prevent overtraining and ensure optimal recovery. \n\nAdjusting the workload when needed is essential for long-term progress and avoiding burnout. \nThat said, if you're feeling up for the challenge, best of luck, give it your all and do your best!\n\nWould you like to lower the difficulty level?", "Yes", "No");
                                if (descission)
                                {
                                    level--;
                                }
                            }
                            else if (currentLevel == "Already lowered level")   
                            {
                                ;
                            }
                            else if (currentLevel == "Already higher level")    
                            {
                                bool descission = await App.Current.MainPage.DisplayAlert("Recommendation to decrease the difficulty", "Based on your recent performance, we suggest decreasing the difficulty of this workout to help prevent overtraining and ensure optimal recovery. \n\nAdjusting the workload when needed is essential for long-term progress and avoiding burnout. \nThat said, if you're feeling up for the challenge, best of luck, give it your all and do your best!\n\nWould you like to lower the difficulty level?", "Yes", "No");
                                if (descission)
                                {
                                    level--;
                                    level--;
                                }
                            }
                        }
                        else if (averageIntensity <= 3.0)   //    Intensity is TOO LOW; we want a higher level
                        {
                            if (currentLevel == "Normal level")
                            {
                                bool descission = await App.Current.MainPage.DisplayAlert("Recommendation to increase the difficulty", "Based on your recent performance, we suggest increasing the difficulty of this workout to help you progress. \nGradual workload adjustments are essential for continuous improvement. \nWould you like to proceed with a higher difficulty level?", "Yes", "No");
                                if (descission)
                                {
                                    level++;
                                }
                            }
                            else if (currentLevel == "Already lowered level")
                            {
                                bool descission = await App.Current.MainPage.DisplayAlert("Recommendation to increase the difficulty", "Based on your recent performance, we suggest increasing the difficulty of this workout to help you progress. \nGradual workload adjustments are essential for continuous improvement. \nWould you like to proceed with a higher difficulty level?", "Yes", "No");
                                if (descission)
                                {
                                    level++;
                                    level++;
                                }
                            }
                            else if (currentLevel == "Already higher level")
                            {
                                ;
                            }
                        }
                    }   //  ------------    -------------   ---------
                    else if (dailyFeel == "Above Average") // DF = 4    We want intensity between: (3,25 ; 3,75)
                    {
                        if (3.25 < averageIntensity && averageIntensity < 3.75) //    Intensity is OK; we want average level
                        {
                            if (currentLevel == "Normal level")
                            {
                                ;
                            }
                            else if (currentLevel == "Already lowered level")   // We want average level but level is lower so we must increase level
                            {
                                bool descission = await App.Current.MainPage.DisplayAlert("Recommendation to increase the difficulty", "Based on your recent performance, we suggest increasing the difficulty of this workout to help you progress. \nGradual workload adjustments are essential for continuous improvement. \nWould you like to proceed with a higher difficulty level?", "Yes", "No");
                                if (descission)
                                {
                                    level++;
                                }
                            }
                            else if (currentLevel == "Already higher level")    // We want average level but level is higher so we must lower level
                            {
                                bool descission = await App.Current.MainPage.DisplayAlert("Recommendation to decrease the difficulty", "Based on your recent performance, we suggest decreasing the difficulty of this workout to help prevent overtraining and ensure optimal recovery. \n\nAdjusting the workload when needed is essential for long-term progress and avoiding burnout. \nThat said, if you're feeling up for the challenge, best of luck, give it your all and do your best!\n\nWould you like to lower the difficulty level?", "Yes", "No");
                                if (descission)
                                {
                                    level--;
                                }
                            }
                        }
                        else if (3.75 <= averageIntensity)   //    Intensity is TOO HIGH; we want to lower level
                        {
                            if (currentLevel == "Normal level")
                            {
                                bool descission = await App.Current.MainPage.DisplayAlert("Recommendation to decrease the difficulty", "Based on your recent performance, we suggest decreasing the difficulty of this workout to help prevent overtraining and ensure optimal recovery. \n\nAdjusting the workload when needed is essential for long-term progress and avoiding burnout. \nThat said, if you're feeling up for the challenge, best of luck, give it your all and do your best!\n\nWould you like to lower the difficulty level?", "Yes", "No");
                                if (descission)
                                {
                                    level--;
                                }
                            }
                            else if (currentLevel == "Already lowered level")
                            {
                                ;
                            }
                            else if (currentLevel == "Already higher level")
                            {
                                bool descission = await App.Current.MainPage.DisplayAlert("Recommendation to decrease the difficulty", "Based on your recent performance, we suggest decreasing the difficulty of this workout to help prevent overtraining and ensure optimal recovery. \n\nAdjusting the workload when needed is essential for long-term progress and avoiding burnout. \nThat said, if you're feeling up for the challenge, best of luck, give it your all and do your best!\n\nWould you like to lower the difficulty level?", "Yes", "No");
                                if (descission)
                                {
                                    level--;
                                    level--;
                                }
                            }
                        }
                        else if (averageIntensity <= 3.25)   //    Intensity is TOO LOW; we want a higher level
                        {
                            if (currentLevel == "Normal level")
                            {
                                bool descission = await App.Current.MainPage.DisplayAlert("Recommendation to increase the difficulty", "Based on your recent performance, we suggest increasing the difficulty of this workout to help you progress. \nGradual workload adjustments are essential for continuous improvement. \nWould you like to proceed with a higher difficulty level?", "Yes", "No");
                                if (descission)
                                {
                                    level++;
                                }
                            }
                            else if (currentLevel == "Already lowered level")
                            {
                                bool descission = await App.Current.MainPage.DisplayAlert("Recommendation to increase the difficulty", "Based on your recent performance, we suggest increasing the difficulty of this workout to help you progress. \nGradual workload adjustments are essential for continuous improvement. \nWould you like to proceed with a higher difficulty level?", "Yes", "No");
                                if (descission)
                                {
                                    level++;
                                    level++;
                                }
                            }
                            else if (currentLevel == "Already higher level")
                            {
                                ;
                            }
                        }
                    }   //  ---------   --------------  --------------
                    else if (dailyFeel == "Below Average") // DF = 2    We want intensity between: (2,75 ; 3,25)
                    {
                        if (2.75 < averageIntensity && averageIntensity < 3.25) //    Intensity is OK; we want average level
                        {
                            if (currentLevel == "Normal level")
                            {
                                ;
                            }
                            else if (currentLevel == "Already lowered level")   // We want average level but level is lower so we must increase level
                            {
                                bool descission = await App.Current.MainPage.DisplayAlert("Recommendation to increase the difficulty", "Based on your recent performance, we suggest increasing the difficulty of this workout to help you progress. \nGradual workload adjustments are essential for continuous improvement. \nWould you like to proceed with a higher difficulty level?", "Yes", "No");
                                if (descission)
                                {
                                    level++;
                                }
                            }
                            else if (currentLevel == "Already higher level")    // We want average level but level is higher so we must lower level
                            {
                                bool descission = await App.Current.MainPage.DisplayAlert("Recommendation to decrease the difficulty", "Based on your recent performance, we suggest decreasing the difficulty of this workout to help prevent overtraining and ensure optimal recovery. \n\nAdjusting the workload when needed is essential for long-term progress and avoiding burnout. \nThat said, if you're feeling up for the challenge, best of luck, give it your all and do your best!\n\nWould you like to lower the difficulty level?", "Yes", "No");
                                if (descission)
                                {
                                    level--;
                                }
                            }
                        }
                        else if (3.25 <= averageIntensity)   //    Intensity is TOO HIGH; we want to lower level
                        {
                            if (currentLevel == "Normal level")
                            {
                                bool descission = await App.Current.MainPage.DisplayAlert("Recommendation to decrease the difficulty", "Based on your recent performance, we suggest decreasing the difficulty of this workout to help prevent overtraining and ensure optimal recovery. \n\nAdjusting the workload when needed is essential for long-term progress and avoiding burnout. \nThat said, if you're feeling up for the challenge, best of luck, give it your all and do your best!\n\nWould you like to lower the difficulty level?", "Yes", "No");
                                if (descission)
                                {
                                    level--;
                                }
                            }
                            else if (currentLevel == "Already lowered level")
                            {
                                ;
                            }
                            else if (currentLevel == "Already higher level")
                            {
                                bool descission = await App.Current.MainPage.DisplayAlert("Recommendation to decrease the difficulty", "Based on your recent performance, we suggest decreasing the difficulty of this workout to help prevent overtraining and ensure optimal recovery. \n\nAdjusting the workload when needed is essential for long-term progress and avoiding burnout. \nThat said, if you're feeling up for the challenge, best of luck, give it your all and do your best!\n\nWould you like to lower the difficulty level?", "Yes", "No");
                                if (descission)
                                {
                                    level--;
                                    level--;
                                }
                            }
                        }
                        else if (averageIntensity <= 2.75)   //    Intensity is TOO LOW; we want a higher level
                        {
                            if (currentLevel == "Normal level")
                            {
                                bool descission = await App.Current.MainPage.DisplayAlert("Recommendation to increase the difficulty", "Based on your recent performance, we suggest increasing the difficulty of this workout to help you progress. \nGradual workload adjustments are essential for continuous improvement. \nWould you like to proceed with a higher difficulty level?", "Yes", "No");
                                if (descission)
                                {
                                    level++;
                                }
                            }
                            else if (currentLevel == "Already lowered level")
                            {
                                bool descission = await App.Current.MainPage.DisplayAlert("Recommendation to increase the difficulty", "Based on your recent performance, we suggest increasing the difficulty of this workout to help you progress. \nGradual workload adjustments are essential for continuous improvement. \nWould you like to proceed with a higher difficulty level?", "Yes", "No");
                                if (descission)
                                {
                                    level++;
                                    level++;
                                }
                            }
                            else if (currentLevel == "Already higher level")
                            {
                                ;
                            }
                        }
                    }   //  --------    ----------  ----------
                    else if (dailyFeel == "Outstanding") // DF = 5  We want intensity between: (3,5 ; 5)
                    {
                        if (3.5 < averageIntensity && averageIntensity < 5) //    Intensity is OK; we want average level
                        {
                            if (currentLevel == "Normal level")
                            {
                                ;
                            }
                            else if (currentLevel == "Already lowered level")   // We want average level but level is lower so we must increase level
                            {
                                bool descission = await App.Current.MainPage.DisplayAlert("Recommendation to increase the difficulty", "Based on your recent performance, we suggest increasing the difficulty of this workout to help you progress. \nGradual workload adjustments are essential for continuous improvement. \nWould you like to proceed with a higher difficulty level?", "Yes", "No");
                                if (descission)
                                {
                                    level++;
                                }
                            }
                            else if (currentLevel == "Already higher level")    // We want average level but level is higher so we must lower level
                            {
                                bool descission = await App.Current.MainPage.DisplayAlert("Recommendation to decrease the difficulty", "Based on your recent performance, we suggest decreasing the difficulty of this workout to help prevent overtraining and ensure optimal recovery. \n\nAdjusting the workload when needed is essential for long-term progress and avoiding burnout. \nThat said, if you're feeling up for the challenge, best of luck, give it your all and do your best!\n\nWould you like to lower the difficulty level?", "Yes", "No");
                                if (descission)
                                {
                                    level--;
                                }
                            }
                        }
                        else if (5 <= averageIntensity)   //    Intensity is TOO HIGH; we want to lower level
                        {
                            if (currentLevel == "Normal level")
                            {
                                bool descission = await App.Current.MainPage.DisplayAlert("Recommendation to decrease the difficulty", "Based on your recent performance, we suggest decreasing the difficulty of this workout to help prevent overtraining and ensure optimal recovery. \n\nAdjusting the workload when needed is essential for long-term progress and avoiding burnout. \nThat said, if you're feeling up for the challenge, best of luck, give it your all and do your best!\n\nWould you like to lower the difficulty level?", "Yes", "No");
                                if (descission)
                                {
                                    level--;
                                }
                            }
                            else if (currentLevel == "Already lowered level")
                            {
                                ;
                            }
                            else if (currentLevel == "Already higher level")
                            {
                                bool descission = await App.Current.MainPage.DisplayAlert("Recommendation to decrease the difficulty", "Based on your recent performance, we suggest decreasing the difficulty of this workout to help prevent overtraining and ensure optimal recovery. \n\nAdjusting the workload when needed is essential for long-term progress and avoiding burnout. \nThat said, if you're feeling up for the challenge, best of luck, give it your all and do your best!\n\nWould you like to lower the difficulty level?", "Yes", "No");
                                if (descission)
                                {
                                    level--;
                                    level--;
                                }
                            }
                        }
                        else if (averageIntensity <= 3.5)   //    Intensity is TOO LOW; we want a higher level
                        {
                            if (currentLevel == "Normal level")
                            {
                                bool descission = await App.Current.MainPage.DisplayAlert("Recommendation to increase the difficulty", "Based on your recent performance, we suggest increasing the difficulty of this workout to help you progress. \nGradual workload adjustments are essential for continuous improvement. \nWould you like to proceed with a higher difficulty level?", "Yes", "No");
                                if (descission)
                                {
                                    level++;
                                }
                            }
                            else if (currentLevel == "Already lowered level")
                            {
                                bool descission = await App.Current.MainPage.DisplayAlert("Recommendation to increase the difficulty", "Based on your recent performance, we suggest increasing the difficulty of this workout to help you progress. \nGradual workload adjustments are essential for continuous improvement. \nWould you like to proceed with a higher difficulty level?", "Yes", "No");
                                if (descission)
                                {
                                    level++;
                                    level++;
                                }
                            }
                            else if (currentLevel == "Already higher level")
                            {
                                ;
                            }
                        }
                    }   //  ------  ---------   --------
                    else if (dailyFeel == "Very Poor") // DF = 1    We want intensity between: (0 ; 2,5)
                    {
                        if (0 < averageIntensity && averageIntensity < 2.5) //    Intensity is OK; we want average level
                        {
                            if (currentLevel == "Normal level")
                            {
                                ;
                            }
                            else if (currentLevel == "Already lowered level")   // We want average level but level is lower so we must increase level
                            {
                                bool descission = await App.Current.MainPage.DisplayAlert("Recommendation to increase the difficulty", "Based on your recent performance, we suggest increasing the difficulty of this workout to help you progress. \nGradual workload adjustments are essential for continuous improvement. \nWould you like to proceed with a higher difficulty level?", "Yes", "No");
                                if (descission)
                                {
                                    level++;
                                }
                            }
                            else if (currentLevel == "Already higher level")    // We want average level but level is higher so we must lower level
                            {
                                bool descission = await App.Current.MainPage.DisplayAlert("Recommendation to decrease the difficulty", "Based on your recent performance, we suggest decreasing the difficulty of this workout to help prevent overtraining and ensure optimal recovery. \nAdjusting the workload when needed is essential for long-term progress and avoiding burnout. \nWould you like to lower the difficulty level?", "Yes", "No");
                                if (descission)
                                {
                                    level--;
                                }
                            }
                        }
                        else if (2.5 <= averageIntensity)   //    Intensity is TOO HIGH; we want to lower level
                        {
                            if (currentLevel == "Normal level")
                            {
                                bool descission = await App.Current.MainPage.DisplayAlert("Recommendation to decrease the difficulty", "Based on your recent performance, we suggest decreasing the difficulty of this workout to help prevent overtraining and ensure optimal recovery. \nAdjusting the workload when needed is essential for long-term progress and avoiding burnout. \nWould you like to lower the difficulty level?", "Yes", "No");
                                if (descission)
                                {
                                    level--;
                                }
                            }
                            else if (currentLevel == "Already lowered level")
                            {
                                ;
                            }
                            else if (currentLevel == "Already higher level")
                            {
                                bool descission = await App.Current.MainPage.DisplayAlert("Recommendation to decrease the difficulty", "Based on your recent performance, we suggest decreasing the difficulty of this workout to help prevent overtraining and ensure optimal recovery. \nAdjusting the workload when needed is essential for long-term progress and avoiding burnout. \nWould you like to lower the difficulty level?", "Yes", "No");
                                if (descission)
                                {
                                    level--;
                                    level--;
                                }
                            }
                        }
                        else if (averageIntensity <= 0)   //    Intensity is TOO LOW; we want a higher level
                        {
                            if (currentLevel == "Normal level")
                            {
                                bool descission = await App.Current.MainPage.DisplayAlert("Recommendation to increase the difficulty", "Based on your recent performance, we suggest increasing the difficulty of this workout to help you progress. \nGradual workload adjustments are essential for continuous improvement. \nWould you like to proceed with a higher difficulty level?", "Yes", "No");
                                if (descission)
                                {
                                    level++;
                                }
                            }
                            else if (currentLevel == "Already lowered level")
                            {
                                bool descission = await App.Current.MainPage.DisplayAlert("Recommendation to increase the difficulty", "Based on your recent performance, we suggest increasing the difficulty of this workout to help you progress. \nGradual workload adjustments are essential for continuous improvement. \nWould you like to proceed with a higher difficulty level?", "Yes", "No");
                                if (descission)
                                {
                                    level++;
                                    level++;
                                }
                            }
                            else if (currentLevel == "Already higher level")
                            {
                                ;
                            }
                        }
                    }

                }
                catch(Exception ee) { 
                    //await App.Current.MainPage.DisplayAlert("Error", ee.Message, "ok");
                    ;
                }
            }

            if (level < 1) { level = 1; }
            // --------------- ------------------ ------------------- --------------- -------------- ----------------- ------------------- ---------------- ----------

            try
            {
                IsLoadingVisible = true;
                //string connectionString = "server=localhost;user=root;database=myworkout1;port=3306;password=";
                string connectionString = "server=192.168.*.*;user=*;database=myworkout1;port=*;password=*";

                await using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();

                    Global.equipment = equipment;
                    Global.level = level;
                    Global.numExercises = Int32.Parse(number_of_exercises);

                    string injuryConditions = "";
                    if (neckInjury)
                    {
                        injuryConditions += " AND neck = 0";
                        Global.neck = 1;
                    }
                    else
                    {
                        Global.neck = 0;
                    }
                    if (backInjury)
                    {
                        injuryConditions += " AND back = 0";
                        Global.back = 1;
                    }
                    else
                    {
                        Global.back = 0;
                    }
                    if (chestInjury)
                    {
                        injuryConditions += " AND chest = 0";
                        Global.chest = 1;
                    }
                    else
                    {
                        Global.chest = 0;
                    }
                    if (armInjury)
                    {
                        injuryConditions += " AND arm = 0";
                        Global.arm = 1;
                    }
                    else 
                    {
                        Global.arm = 0;
                    }
                    if (wristInjury)
                    {
                        injuryConditions += " AND wrist = 0";
                        Global.wrist = 1;
                    }
                    else
                    {
                        Global.wrist = 0;
                    }
                    if (abdominalInjury)
                    {
                        injuryConditions += " AND abdominalArea = 0";
                        Global.abdominalArea = 1;
                    }
                    else
                    {
                        Global.abdominalArea = 0;
                    }
                    if (legInjury)
                    {
                        injuryConditions += " AND leg = 0";
                        Global.leg = 1;
                    }
                    else { Global.leg = 0; }
                    if (ankleInjury)
                    {
                        injuryConditions += " AND ankle = 0";
                        Global.ankle = 1;
                    }
                    else { Global.ankle= 0; }


                    if (body_part == "Full body" && (Int32.Parse(number_of_exercises)) >= 3)
                    {
                        
                        cmd.CommandText = $@"
                    (SELECT * FROM exercises WHERE equipment = '{equipment}' AND oftype = '{workout_type}' AND upperBody = 1 AND level = {level} {injuryConditions} ORDER BY RAND() LIMIT {(Int32.Parse(number_of_exercises)) / 3})
                    UNION
                    (SELECT * FROM exercises WHERE equipment = '{equipment}' AND oftype = '{workout_type}' AND coreBody = 1 AND level = {level} {injuryConditions} ORDER BY RAND() LIMIT {(Int32.Parse(number_of_exercises)) / 3})
                    UNION
                    (SELECT * FROM exercises WHERE equipment = '{equipment}' AND oftype = '{workout_type}' AND lowerBody = 1 AND level = {level} {injuryConditions} ORDER BY RAND() LIMIT {(Int32.Parse(number_of_exercises)) / 3})";
                        //await App.Current.MainPage.DisplayAlert("TEST IZPIS 0", cmd.CommandText + "WHAAAAAAAAAAAA" + (Int32.Parse(number_of_exercises)) / 3, "OK");
                    }
                    else
                    {
                        //List<string> conditions = new List<string>();
                        //if (body_part == "upper body") conditions.Add("upperBody = 1");
                        //if (body_part == "core") conditions.Add("coreBody = 1");
                        //if (body_part == "lower body") conditions.Add("lowerBody = 1");
                        //string bodyConditions = conditions.Count > 0 ? string.Join(" OR ", conditions) : string.Empty;

                        string bodyTypeConditions = "";
                        if (body_part == "Upper body") bodyTypeConditions += " AND upperBody = 1";
                        if (body_part == "Core") bodyTypeConditions += " AND coreBody = 1";
                        if (body_part == "Lower body") bodyTypeConditions += " AND lowerBody = 1";

                        cmd.CommandText = $@"
                    SELECT * FROM exercises 
                    WHERE (equipment = '{equipment}' AND oftype = '{workout_type}' AND level = {level} {bodyTypeConditions} {injuryConditions})
                    ORDER BY RAND() 
                    LIMIT {number_of_exercises}";
                    }
                    //await App.Current.MainPage.DisplayAlert("TEST IZPIS 1", cmd.CommandText, "OK");
                    MySqlDataReader reader = cmd.ExecuteReader();
                    Global.exercises.Clear();

                    while (reader.Read())
                    {
                        Exercise exercise = new Exercise
                        {
                            idExercises = reader.GetInt32("idExercises"),
                            name = reader.GetString("name"),
                            repsandsets = reader.GetString("repsandsets"),
                            texttip = reader.GetString("texttip"),
                            //exerciseImage = reader.GetString("exerciseImage"),
                            exerciseImage2 = new MemoryStream(Convert.FromBase64String(reader.GetString("exerciseImage"))),
                            //exerciseImage3 = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(reader.GetString("exerciseImage")))),
                            //muscleImage = reader.GetString("muscleImage"),
                            muscleImage2 = new MemoryStream(Convert.FromBase64String(reader.GetString("muscleImage"))),
                            //muscleImage3 = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(reader.GetString("muscleImage")))),
                            equipment = reader.GetString("equipment"), 
                            oftype = reader.GetString("oftype"),
                            level = reader.GetInt32("level"),
                            upperBody = reader.GetBoolean("upperBody"),
                            coreBody = reader.GetBoolean("coreBody"),
                            lowerBody = reader.GetBoolean("lowerBody")
                        };

                        Global.exercises.Add(exercise);
                    }
                    //await App.Current.MainPage.DisplayAlert("TEST IZPIS 5", Global.exercises.Count.ToString(), "OK");
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                IsLoadingVisible = false;
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            
            await this._navigation.PushAsync(new ExerciseDisplay());
            IsLoadingVisible = false;
        }

    }
}
