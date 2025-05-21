using Test2.ViewModels;

namespace Test2;

public partial class MyWorkout : ContentPage
{
    public static readonly IList<string> EquipmentList = new List<string>
     {
        "Basic gear",
        "No gear",
        "Fitness gear"
     };

    public static readonly IList<string> WorkoutTypeList = new List<string>
    {
      "Strength",
      "Cardio",
      "Endurance"
    };

    public static readonly IList<string> BodyPartList = new List<string>
    {
      "Upper body",
      "Full body",
      "Core",
      "Chest",
      "Arms",
      "Lower body",
      "Legs"
    };

    public static readonly IList<string> DifficultyList = new List<string>
    {
      "Level I - Light",
      "Level II - Easy",
      "Level III - Normal",
      "Level IV - Hard",
      "Level V - Advanced"
    };

    public static readonly IList<string> ExerciseCountList = new List<string>
    {
      "1",
      "2",
      "3",
      "4",
      "5",
      "6"
    };

    public static readonly IList<string> InjuriesList = new List<string>
    {
      "Neck",
      "Arms",
      "Wrists",
      "Back",
      "Lower back",
      "Ankles"
    };

    private void ToggleInjuryVisibility(object sender, EventArgs e)
    {
        if (InjuryCheckboxContainer != null)
        {
            InjuryCheckboxContainer.IsVisible = !InjuryCheckboxContainer.IsVisible;
        }
    }

    private void OnInjuryCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (InjuryButton != null)
        {
            int count = 0;
            if (NeckInjury?.IsChecked == true) count++;
            if (BackInjury?.IsChecked == true) count++;
            if (ChestInjury?.IsChecked == true) count++;
            if (ArmInjury?.IsChecked == true) count++;
            if (WristInjury?.IsChecked == true) count++;
            if (AbdominalInjury?.IsChecked == true) count++;
            if (LegInjury?.IsChecked == true) count++;
            if (AnkleInjury?.IsChecked == true) count++;

            InjuryButton.Text = $"Injuries ({count})";
        }
    }

    private List<string> GetSelectedInjuries()
    {
        List<string> injuries = new List<string>();
        if (NeckInjury?.IsChecked == true) injuries.Add("neck");
        if (BackInjury?.IsChecked == true) injuries.Add("back");
        if (ChestInjury?.IsChecked == true) injuries.Add("chest");
        if (ArmInjury?.IsChecked == true) injuries.Add("arm");
        if (WristInjury?.IsChecked == true) injuries.Add("wrist");
        if (AbdominalInjury?.IsChecked == true) injuries.Add("abdominalArea");
        if (LegInjury?.IsChecked == true) injuries.Add("leg");
        if (AnkleInjury?.IsChecked == true) injuries.Add("ankle");
        return injuries;
    }


    private async void StartWorkout(object sender, EventArgs e)
    {
        if (BindingContext is MyWorkoutViewModel viewModel)
        {
            viewModel.SelectedInjuries = GetSelectedInjuries();
            viewModel.StartButton?.Execute(null);
        }
    }

    


    public MyWorkout()
    {
        //bool Neck = NeckInjury.IsChecked;
        Application.Current.UserAppTheme = AppTheme.Dark;
        InitializeComponent();
        BindingContext = new MyWorkoutViewModel(Navigation);
    }
}
