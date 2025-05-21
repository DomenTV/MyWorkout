using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using LiteDB;
using Test2.Models;


namespace Test2.ViewModels
{
    internal class WorkoutsDisplayViewModel
    {
       
            private INavigation _navigation;
            public string name { get; set; }
            public string repsandsets { get; set; }
            public string texttip { get; set; }
            public string exerciseImage { get; set; }
            public string muscleImage { get; set; }
            public string equpment { get; set; }
            public string oftype { get; set; }
            public string bodyParts { get; set; }
            public int level { get; set; }
            public string bodyPart { get; set; }
            public List<Exercise> exerciseList { get; set; }
            public List<Exercise> exerciseList2 { get; set; }
            public int width { get; set; }
            public Command HowToBtn { get; }
            public string muscleImage1 { get; set; }
            public string muscleImage2 { get; set; }
            public string muscleImage3 { get; set; }
            public string muscleImage4 { get; set; }
            public string muscleImage5 { get; set; }
            public string muscleImage6 { get; set; }
            public TimeOnly theTimeCounter = new();
            public string theTime { get; set; }
            public bool isRunning { get; set; }
            public Command StopButton { get; }

            public WorkoutsDisplayViewModel(INavigation navigation)
            {

                this._navigation = navigation;
                exerciseList = Global.exercises;
                exerciseList2 = Global.exercises;
                width = Convert.ToInt32(Math.Round(DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density * 0.9));
                HowToBtn = new Command(HowToBtnTappedAsync);
                isRunning = true;

            }

            private async void HowToBtnTappedAsync(object obj)
            {
                isRunning = false;
                //Counting();
                await App.Current.MainPage.DisplayAlert("INFO", "The application will only show 100 records at a time for application performance reasons.", "OK");
                isRunning = true;
            }
            private async void Counting()
            {
                isRunning = true;
                while (isRunning)
                {
                    theTimeCounter = theTimeCounter.Add(TimeSpan.FromSeconds(1));
                    SetTime();
                    await Task.Delay(TimeSpan.FromSeconds(1));
                }
            }
            private void SetTime()
            {
                theTime = $"{theTimeCounter.Hour}:{theTimeCounter.Minute}:{theTimeCounter.Second}";
            }
            private async void StopButtonTappedAsync(object obj)
            {

                await this._navigation.PushAsync(new MyWorkout());
            }
        }
    }

