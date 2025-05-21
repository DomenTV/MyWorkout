//using AndroidX.ConstraintLayout.Core.State;
using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test2.Controllers;
using Test2.Models;
using System.Text.Json;
using Newtonsoft.Json;
//using static Android.Content.ClipData;

namespace Test2.ViewModels
{
    //static class TheList
    //{
    //    public static List<Predmet> zbirkaPredmetov = new List<Predmet>();
    //}
    class Predmet
    {
        public string Naziv { get; set; }
        public Guid PredmetID { get; set; }
        public int ECTS { get; set; }
        public string Semester { get; set; }
        

        public Predmet(string Naziv, Guid PredmetID, int ECTS, string Semester)
        {
            this.Naziv = Naziv;
            this.PredmetID = PredmetID;
            this.ECTS = ECTS;
            this.Semester = Semester;
        }
    }
    internal class SyllabusPageViewModel
    {
        private INavigation _navigation;
        public Command TestBtn { get; }
        public List<Predmet> zbirkaPredmetov = new List<Predmet>();
        public string testing {get; set;}

        public SyllabusPageViewModel(INavigation navigation)
        {
            this._navigation = navigation;
            TestBtn = new Command(TestBtnTappedAsync);
            this.testing = "Groot";
            //GetData();

            //int st = 1;
            //string hi = "-> ";
            //List<Predmet> zbirkaPredmetov = new List<Predmet>();
            ////HER1:
            //FirebaseClient firebaseClient = new FirebaseClient(baseUrl: "https://rmr-2022-c539c-default-rtdb.europe-west1.firebasedatabase.app/");
            ////hi = hi + firebaseClient.Child("predmeti").AsObservable<Predmet>().Subscribe().ToString();
            //var collection = firebaseClient.Child("predmeti").AsObservable<Predmet>().Subscribe((item) =>
            //{
            //    hi = hi + item.Object.Naziv;
            //    hi = hi + " yo ";
            //    st++;
            //    if (item.Object != null)
            //    {
            //        st++;
            //        //await App.Current.MainPage.DisplayAlert("IF stavek", item.Object.Naziv, "ok");
            //        zbirkaPredmetov.Add(new Predmet(item.Object.Naziv, item.Object.PredmetID, item.Object.ECTS, item.Object.Semester));
            //    }
            //});
            ////goto HER1;
            //App.Current.MainPage.DisplayAlert("IF stavek", zbirkaPredmetov.Count.ToString(), "ok");
            
        }

        private async void GetData()
        {
            int st = 1;
            string hi = "-> ";
            //List<Predmet> zbirkaPredmetov = new List<Predmet>();
            //HER1:
            //FirebaseClient firebaseClient = new FirebaseClient(baseUrl: "https://rmr-2022-c539c-default-rtdb.europe-west1.firebasedatabase.app/");
            //hi = hi + firebaseClient.Child("predmeti").AsObservable<Predmet>().Subscribe().ToString();
            var collection = Global.firebaseClient.Child("predmeti").AsObservable<Predmet>().Subscribe((item) =>
            {
                hi = hi + item.Object.Naziv;
                hi = hi + " yo ";
                st++;
                if (item.Object != null)
                {
                    st++;
                    //await App.Current.MainPage.DisplayAlert("IF stavek", item.Object.Naziv, "ok");
                    zbirkaPredmetov.Add(new Predmet(item.Object.Naziv, item.Object.PredmetID, item.Object.ECTS, item.Object.Semester));
                }
            });
            //goto HER1;
            //await App.Current.MainPage.DisplayAlert("this: ", TheList.zbirkaPredmetov[0].Naziv, "ahh");
        }
        private async void TestBtnTappedAsync(object obj)
        {
            //await App.Current.MainPage.DisplayAlert("this: ", zbirkaPredmetov[0].Naziv, "ahh");

            //Items = new List<TodoItem>();
            HttpClient _client = new HttpClient();
            JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            Uri uri = new Uri("https://dummy.restapiexample.com/api/v1/employees");
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    //string testing = "[{\"id\":1,\"employee_name\":\"Tiger Nixon\",\"employee_salary\":320800,\"employee_age\":61,\"profile_image\":\"\"},{\"id\":2,\"employee_name\":\"Garrett Winters\",\"employee_salary\":170750,\"employee_age\":63,\"profile_image\":\"\"},{\"id\":3,\"employee_name\":\"Ashton Cox\",\"employee_salary\":86000,\"employee_age\":66,\"profile_image\":\"\"},{\"id\":4,\"employee_name\":\"Cedric Kelly\",\"employee_salary\":433060,\"employee_age\":22,\"profile_image\":\"\"},{\"id\":5,\"employee_name\":\"Airi Satou\",\"employee_salary\":162700,\"employee_age\":33,\"profile_image\":\"\"},{\"id\":6,\"employee_name\":\"Brielle Williamson\",\"employee_salary\":372000,\"employee_age\":61,\"profile_image\":\"\"},{\"id\":7,\"employee_name\":\"Herrod Chandler\",\"employee_salary\":137500,\"employee_age\":59,\"profile_image\":\"\"},{\"id\":8,\"employee_name\":\"Rhona Davidson\",\"employee_salary\":327900,\"employee_age\":55,\"profile_image\":\"\"},{\"id\":9,\"employee_name\":\"Colleen Hurst\",\"employee_salary\":205500,\"employee_age\":39,\"profile_image\":\"\"},{\"id\":10,\"employee_name\":\"Sonya Frost\",\"employee_salary\":103600,\"employee_age\":23,\"profile_image\":\"\"},{\"id\":11,\"employee_name\":\"Jena Gaines\",\"employee_salary\":90560,\"employee_age\":30,\"profile_image\":\"\"},{\"id\":12,\"employee_name\":\"Quinn Flynn\",\"employee_salary\":342000,\"employee_age\":22,\"profile_image\":\"\"},{\"id\":13,\"employee_name\":\"Charde Marshall\",\"employee_salary\":470600,\"employee_age\":36,\"profile_image\":\"\"},{\"id\":14,\"employee_name\":\"Haley Kennedy\",\"employee_salary\":313500,\"employee_age\":43,\"profile_image\":\"\"},{\"id\":15,\"employee_name\":\"Tatyana Fitzpatrick\",\"employee_salary\":385750,\"employee_age\":19,\"profile_image\":\"\"},{\"id\":16,\"employee_name\":\"Michael Silva\",\"employee_salary\":198500,\"employee_age\":66,\"profile_image\":\"\"},{\"id\":17,\"employee_name\":\"Paul Byrd\",\"employee_salary\":725000,\"employee_age\":64,\"profile_image\":\"\"},{\"id\":18,\"employee_name\":\"Gloria Little\",\"employee_salary\":237500,\"employee_age\":59,\"profile_image\":\"\"},{\"id\":19,\"employee_name\":\"Bradley Greer\",\"employee_salary\":132000,\"employee_age\":41,\"profile_image\":\"\"},{\"id\":20,\"employee_name\":\"Dai Rios\",\"employee_salary\":217500,\"employee_age\":35,\"profile_image\":\"\"},{\"id\":21,\"employee_name\":\"Jenette Caldwell\",\"employee_salary\":345000,\"employee_age\":30,\"profile_image\":\"\"},{\"id\":22,\"employee_name\":\"Yuri Berry\",\"employee_salary\":675000,\"employee_age\":40,\"profile_image\":\"\"},{\"id\":23,\"employee_name\":\"Caesar Vance\",\"employee_salary\":106450,\"employee_age\":21,\"profile_image\":\"\"},{\"id\":24,\"employee_name\":\"Doris Wilder\",\"employee_salary\":85600,\"employee_age\":23,\"profile_image\":\"\"}]";

                    int startindex = content.IndexOf("[");
                    int endindex = content.IndexOf("]");
                    string outputString = content.Substring(startindex, endindex - startindex + 1);

                    //Data a = JsonConvert.DeserializeObject<Data>(content);
                    //await App.Current.MainPage.DisplayAlert("outputString ", outputString, "ok");
                    //await App.Current.MainPage.DisplayAlert("testing: ", testing, "ok");
                    Global.employeesList = JsonConvert.DeserializeObject<List<Employee>>(outputString);
                    Global.employeesList = Global.employeesList.Take(20).ToList();
                    //Global.employeesList.Add(tst);

                    //await App.Current.MainPage.DisplayAlert("The String: ", content, "ok");
                    //Global.employeesList = System.Text.Json.JsonSerializer.Deserialize<List<Employee>>(content, _serializerOptions);
                    await this._navigation.PushAsync(new EmployeesPage());
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error: ", ex.Message, "ok");
            }


            //await this._navigation.PushAsync(new EmployeesPage());
            //return View(EmployeesPage)
        }
    }
}
