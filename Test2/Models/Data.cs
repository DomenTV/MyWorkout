using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2.Models
{
    internal class Data
    {
        [JsonProperty("status")]
        public string status { get; set; }
        [JsonProperty("data")]
        public string data { get; set; }
    }
}
