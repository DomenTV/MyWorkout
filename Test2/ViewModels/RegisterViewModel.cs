using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using MySql.Data.MySqlClient;
using CommunityToolkit.Mvvm.ComponentModel;


namespace Test2.ViewModels
{
    internal partial class RegisterViewModel : ObservableObject
    {
        public string webApiKey = "*";
        //public string webApiKey = "*";
        private INavigation _navigation;

        public Command RegisterUser { get; }
        public Command LoginBtnBack { get; }
        public string email { get; set; }
        public string password { get; set; }
        [ObservableProperty]
        private bool isLoadingVisible;

        public RegisterViewModel(INavigation navigation)
        {
            this._navigation = navigation;
            IsLoadingVisible = false;
            RegisterUser = new Command(RegisterUserTappedAsync);
            LoginBtnBack = new Command(LoginBtnBackTappedAsync);
        }

        private async void LoginBtnBackTappedAsync(object obj)
        {
            await this._navigation.PushAsync(new MainPage());
        }

        private async void RegisterUserTappedAsync(object obj)
        {
            //string _connectionString1 = "server=localhost;user=root;database=myworkout1;port=3306;password=";
            ////MYSQL database add user
            //using (var connection1 = new MySqlConnection(_connectionString1))
            //{
            //    connection1.Open();

            //    var query1 = "INSERT INTO Users (email) VALUES (@email)";
            //    using (var command = new MySqlCommand(query1, connection1))
            //    {
            //        command.Parameters.AddWithValue("@email", email);
            //        command.ExecuteNonQuery();
            //        await App.Current.MainPage.DisplayAlert("Success!", query1, email);
            //    }
            //}
            IsLoadingVisible = true;
            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
                string token = auth.FirebaseToken;
                if (token != null)
                {
                    await App.Current.MainPage.DisplayAlert("Success!", "User registered successfully", "Return to login");
                    string _connectionString = "server=192.168.*.*;user=*;database=myworkout1;port=*;password=*";
                    //MYSQL database add user
                    using (var connection = new MySqlConnection(_connectionString))
                    {
                        connection.Open();

                        var query = "INSERT INTO users (email) VALUES (@email)";
                        using (var command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@email", email);
                            command.ExecuteNonQuery();
                        }
                    }


                    IsLoadingVisible = false;
                    await this._navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert!!!", ex.Message, "okay");
                //throw;
                
            }
            finally
            {
                IsLoadingVisible = false;
            }
            
        }
    }
}
