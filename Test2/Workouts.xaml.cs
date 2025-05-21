using Test2.ViewModels;

namespace Test2;

public partial class Workouts : ContentPage
{
	public Workouts()
	{
        InitializeComponent();
        BindingContext = new
        WorkoutsViewModel(Navigation);
    }
}