using Test2.ViewModels;

namespace Test2;

public partial class WorkoutsDisplay : ContentPage
{
	public WorkoutsDisplay()
	{
        InitializeComponent();
        BindingContext = new
        WorkoutsDisplayViewModel(Navigation);
    }
}