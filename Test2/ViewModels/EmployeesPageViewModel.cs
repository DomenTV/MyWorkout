using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test2.Models;

namespace Test2.ViewModels
{
    internal class EmployeesPageViewModel
    {
        public class Employee1
        {
            [JsonProperty("id")]
            public int id { get; set; }
            [JsonProperty("employee_name")]
            public string employee_name { get; set; }
            [JsonProperty("employee_salary")]
            public int employee_salary { get; set; }
            [JsonProperty("employee_age")]
            public int employee_age { get; set; }
            [JsonProperty("profile_image")]
            public string profile_image { get; set; }
        }
        public List<Employee> theList { get; set; }
        private INavigation _navigation;
        public string employee_name { get; set; }
        public int id { get; set; }


        public EmployeesPageViewModel(INavigation navigation)
        {
            this._navigation = navigation;
            theList = Global.employeesList;
        }
    }
}
