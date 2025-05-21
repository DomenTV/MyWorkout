using Test2.ViewModels;

namespace Test2;

public partial class User : ContentPage
{
	public User()
	{
        InitializeComponent();
        BindingContext = new
        UserViewModel(Navigation);
        //UserNameLabel.Text = Global.globalEmailName.ToUpper() + "'s past workouts".ToUpper();
        UserNameLabel.Text = "Your Past Workouts";
    }
}