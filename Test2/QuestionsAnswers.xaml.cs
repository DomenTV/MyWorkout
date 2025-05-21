using Test2.ViewModels;

namespace Test2;

public partial class QuestionsAnswers : ContentPage
{
	public QuestionsAnswers()
	{
		InitializeComponent();
        BindingContext = new
        QuestionsAnswersViewModel(Navigation); 
    }

    //void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    //{
    //    if (e.SelectedItem != null)
    //    {
    //        // An item was selected, set IsAnswerVisible to true
    //        IsAnswerVisible = true;
    //    }
    //    else
    //    {
    //        // An item was deselected, set IsAnswerVisible to false
    //        IsAnswerVisible = false;
    //    }
    //}

}