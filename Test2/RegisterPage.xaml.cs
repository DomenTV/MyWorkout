using Test2.ViewModels;

namespace Test2;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
        BindingContext = new
		RegisterViewModel(Navigation);
    }
}