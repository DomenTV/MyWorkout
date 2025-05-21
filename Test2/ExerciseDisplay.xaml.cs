using Test2.ViewModels;


namespace Test2;

public partial class ExerciseDisplay : ContentPage
{
	public ExerciseDisplay()
	{
		InitializeComponent();
        BindingContext = new
        ExerciseDisplayViewModel(Navigation);
        
        
    }
}