using Test2.ViewModels;


namespace Test2;

public partial class PastExerciseDisplay : ContentPage
{
	public PastExerciseDisplay()
	{
		InitializeComponent();
        BindingContext = new
        PastExerciseDisplayViewModel(Navigation);
        
        
    }
}