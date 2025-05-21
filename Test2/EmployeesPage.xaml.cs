namespace Test2.Models;
using Test2.Controllers;
using Test2.ViewModels;


public partial class EmployeesPage : ContentPage
{
    public EmployeesPage()
    {
        InitializeComponent();
        BindingContext = new
        EmployeesPageViewModel(Navigation);
        //BindingContext = new
        //EmployeeREST(Navigation);

        //App.Current.MainPage.DisplayAlert("Number of employees: ", Global.employeesList[0].employee_name + " III " + Global.employeesList.Count.ToString(), "ok");

        //theList.BindingContext = Global.employeesList;
        //theList1.ItemsSource = Global.employeesList;
        theList.ItemsSource = Global.employeesList;
    }
}