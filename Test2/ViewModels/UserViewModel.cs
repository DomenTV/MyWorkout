using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Test2.ViewModels
{
    internal class UserViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<PastWorkouts> WorkoutList { get; set; }
        public ICommand ToggleWorkoutDetailsCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool IsLoadingVisible
        {
            get => _isLoadingVisible;
            set
            {
                if (_isLoadingVisible != value)
                {
                    _isLoadingVisible = value;
                    OnPropertyChanged(nameof(IsLoadingVisible));
                }
            }
        }

        public ICommand ShowWorkoutIDCommand { get; }
        private bool _isLoadingVisible;

        private INavigation _navigation;

        public int pastWorkoutID { get; set; }
        public string date { get; set; }
        public string duration { get; set; }
        public string oftype { get; set; }
        public string bodyPart { get; set; }
        public string email { get; set; }
        public string emailName { get; set; }
        public List<PastWorkouts> workoutList { get; set; }
        public int width { get; set; }
        public Command HowToBtn { get; }
        public bool IsDetailsVisible { get; set; }
        public string equipment {  get; set; }
        public int intensity { get; set; }
        public int level { get; set; }
        public int numExercises { get; set; }
        public Command ProfileButton { get; }
        public Command ExercisesButton { get; }
        public Command qaButton { get; }


        public UserViewModel(INavigation navigation)
        {

            this._navigation = navigation;
            workoutList = Global.pastWorkouts;
            width = Convert.ToInt32(Math.Round(DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density * 0.9));
            email = Global.globalEmail;
            emailName = Global.globalEmailName;
            ProfileButton = new Command(ProfileButtonTappedAsync);
            ExercisesButton = new Command(ExercisesButtonTappedAsync);
            qaButton = new Command(qaButtonTappedAsync);


            WorkoutList = new ObservableCollection<PastWorkouts>(Global.pastWorkouts);

            ToggleWorkoutDetailsCommand = new Command<PastWorkouts>((workout) =>
            {
                if (workout != null)
                {
                    workout.IsDetailsVisible = !workout.IsDetailsVisible;
                    OnPropertyChanged(nameof(WorkoutList)); // Refresh UI
                }
            });


            ShowWorkoutIDCommand = new Command<PastWorkouts>(async (workout) =>
            {
                
                var theId = workout.pastWorkoutID;
                try
                {
                    string connectionString = "server=192.168.*.*;user=*;database=myworkout1;port=*;password=*";
                    IsLoadingVisible = true;
                    await Task.Delay(1);
                    await using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        MySqlCommand cmd = connection.CreateCommand();

                        cmd.CommandText = $@"
                SELECT e.* FROM exercises e
                INNER JOIN exercisesofworkouts ew ON e.idExercises = ew.Exercises_idExercises
                WHERE ew.PastWorkouts_idPastWorkouts = {theId}";

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
                                exerciseImage2 = new MemoryStream(Convert.FromBase64String(reader.GetString("exerciseImage"))),
                                muscleImage2 = new MemoryStream(Convert.FromBase64String(reader.GetString("muscleImage"))),
                                equipment = reader.GetString("equipment"),
                                oftype = reader.GetString("oftype"),
                                level = reader.GetInt32("level"),
                                upperBody = reader.GetBoolean("upperBody"),
                                coreBody = reader.GetBoolean("coreBody"),
                                lowerBody = reader.GetBoolean("lowerBody")
                            };

                            Global.exercises.Add(exercise);
                        }

                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                    IsLoadingVisible = false;
                }

                await this._navigation.PushAsync(new PastExerciseDisplay());
                IsLoadingVisible = false;
            });
            IsLoadingVisible = false;
        }
        private async void ExercisesButtonTappedAsync(object obj)
        {
            IsLoadingVisible = true;
            await this._navigation.PushAsync(new MyWorkout());
            IsLoadingVisible = false;
        }
        private async void qaButtonTappedAsync(object obj)
        {
            IsLoadingVisible = true;
            await this._navigation.PushAsync(new FAQ());
            IsLoadingVisible = false;
        }
        private async void ProfileButtonTappedAsync(object obj)
        {
            await this._navigation.PushAsync(new User());
        }
    }
}
