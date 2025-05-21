using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;
using CommunityToolkit.Mvvm.ComponentModel;

using Newtonsoft.Json;




namespace Test2.ViewModels
{

    internal partial class LoginViewModel : ObservableObject
    {
        public string webApiKey = "*";
        //public string webApiKey = "*";
        private INavigation _navigation;
        public Command RegisterBtn { get; }
        public Command LoginBtn { get; }
        public Command TestBtn { get; }
        public string email { get; set; }
        public string password { get; set; }
        [ObservableProperty]
        private bool isLoadingVisible; 



        public LoginViewModel(INavigation navigation)
        {
            this._navigation = navigation;
            IsLoadingVisible = false;
            RegisterBtn = new Command(RegisterBtnTappedAsync);
            LoginBtn = new Command(LoginBtnTappedAsync);
            TestBtn = new Command(TestBtnTappedAsync);
            email = "test@test.com";
            password = "Test998!";
        }

        private async void RegisterBtnTappedAsync(object obj)
        {
            await this._navigation.PushAsync(new RegisterPage());
        }

        private async void LoginBtnTappedAsync(object obj)
        {
            //await App.Current.MainPage.DisplayAlert("Wrong login information!", obj.ToString(), "OK"); 
            //var authProvider = new FirebaseAuthProvider(new FirebaseAuthConfig(webApiKey));

            //email = "test@test.com";
            //password = "Test998!";
            IsLoadingVisible = true;
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
            try
            {
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
                var content = await auth.GetFreshAuthAsync();
                Global.globalEmail = email;
                int atIndex = email.IndexOf('@');
                Global.globalEmailName = email.Substring(0, atIndex);
                var serializedContent = JsonConvert.SerializeObject(content);
                Preferences.Set("FreshFirebaseToken", serializedContent);
                
                // GET THE DATA AFTER LOGIN
                var collection = Global.firebaseClient.Child("predmeti").AsObservable<Predmet>().Subscribe((item) =>
                {
                    if (item.Object != null)
                    {
                        //DisplayAlert("IF stavek", item.Object.Naziv, "ok");
                        Global.zbirkaPredmetov.Add(new Test2.Predmet(item.Object.Naziv, item.Object.PredmetID.ToString(), item.Object.ECTS, item.Object.Semester));
                    }
                });
                IsLoadingVisible = false;
                await this._navigation.PushAsync(new MyWorkout());

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Wrong login information!", ex.Message, "OK");
                
            }
        }
        private async void TestBtnTappedAsync(object obj)
        {
            await App.Current.MainPage.DisplayAlert("this: ", email, "ahh");
        }
    }
}
