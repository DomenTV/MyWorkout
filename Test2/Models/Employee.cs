using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2.Models
{
    public class Employee
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
}
