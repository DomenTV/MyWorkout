using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test2.Models;

namespace Test2.Controllers
{
    internal class EmployeeREST
    {
        public static List<Employee> list = new List<Employee>();
        public List<Employee> GetEmployees()
        {
            

            return list;
        }
    }
}
