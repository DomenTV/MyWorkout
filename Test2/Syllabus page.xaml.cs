using Firebase.Database;
using System.ComponentModel;

//using Javax.Microedition.Khronos.Egl;
using Test2.Models;
using Test2.ViewModels;

namespace Test2;
public static class Global
{
    internal static FirebaseClient firebaseClient = new FirebaseClient(baseUrl: "https://rmr-2022-c539c-default-rtdb.europe-west1.firebasedatabase.app/");
    internal static List<Predmet> zbirkaPredmetov = new List<Predmet>();
    internal static List<Employee> employeesList = new List<Employee>();
    internal static string globalEmail = "";
    internal static string globalEmailName = "";
    internal static FitnessQuestion[] arrayQuestions = new FitnessQuestion[20];
    internal static List<FitnessQuestion> questionsANDanswers = new List<FitnessQuestion>();
    internal static List<Exercise> exercises = new List<Exercise>();
    internal static string oftype = "";
    internal static string bodyPart = "";
    internal static List<PastWorkouts> pastWorkouts = new List<PastWorkouts>();
    internal static string equipment = "";
    internal static int level = 0;
    internal static int numExercises = 0;
    internal static int neck;
    internal static int back;
    internal static int chest;
    internal static int arm;
    internal static int wrist;
    internal static int abdominalArea;
    internal static int leg;
    internal static int ankle;
    internal static int recommendations;
}
public class FitnessQuestion
{
    public string question { get; set; }
    public string answer { get; set; }
}
public class PastWorkouts : INotifyPropertyChanged
{
    public int pastWorkoutID { get; set; }
    public string date { get; set; }
    public string duration { get; set; }
    public int intensity { get; set; }
    public string equipment { get; set; }
    public string oftype { get; set; }
    public string bodyPart { get; set; }
    public int level { get; set; }
    public int numExercises { get; set; }
    public int neck { get; set; }
    internal int back { get; set; }
    internal int chest { get; set; }
    internal int arm { get; set; }
    internal int wrist { get; set; }
    internal int abdominalArea { get; set; }
    internal int leg { get; set; }
    internal int ankle { get; set; }
    internal int recommendations { get; set; }



    private bool _isDetailsVisible;
    public bool IsDetailsVisible
    {
        get => _isDetailsVisible;
        set
        {
            if (_isDetailsVisible != value)
            {
                _isDetailsVisible = value;
                OnPropertyChanged(nameof(IsDetailsVisible));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
public class Predmet
{
	public string Naziv { get; set; }
	public string PredmetID { get; set; }
	public int ECTS { get; set; }
	public string Semester { get; set; }

	public Predmet(string Naziv, string PredmetID, int ECTS, string Semester)
	{
		this.Naziv = Naziv;
		this.PredmetID = PredmetID;
		this.ECTS = ECTS;
		this.Semester = Semester;
	}
}

public partial class Syllabus_page : ContentPage
{
	public Syllabus_page()
	{
        //List<Predmet> zbirkaPredmetov = new List<Predmet>();
        //FirebaseClient firebaseClient = new FirebaseClient(baseUrl: "https://rmr-2022-c539c-default-rtdb.europe-west1.firebasedatabase.app/");
		//Test.Text = "Hi";
        //var collection = Global.firebaseClient.Child("predmeti").AsObservable<Predmet>().Subscribe((item) =>
        //{
        //    if (item.Object != null)
        //    {
        //        DisplayAlert("IF stavek", item.Object.Naziv, "ok");
        //        Global.zbirkaPredmetov.Add(new Predmet(item.Object.Naziv, item.Object.PredmetID, item.Object.ECTS, item.Object.Semester));
        //    }
        //});
        InitializeComponent();
		BindingContext = new
		SyllabusPageViewModel(Navigation);
		//label1.Text = Global.zbirkaPredmetov[0].Naziv;
		//DisplayAlert("hi", Global.zbirkaPredmetov[0].Naziv.ToString(), "ok");
		//n1.Text = Global.zbirkaPredmetov[0].Naziv;
		//e1.Text = Global.zbirkaPredmetov[0].ECTS.ToString();

  //      n2.Text = Global.zbirkaPredmetov[1].Naziv;
  //      e2.Text = Global.zbirkaPredmetov[1].ECTS.ToString();

  //      n3.Text = Global.zbirkaPredmetov[2].Naziv;
  //      e3.Text = Global.zbirkaPredmetov[2].ECTS.ToString();

  //      n4.Text = Global.zbirkaPredmetov[3].Naziv;
  //      e4.Text = Global.zbirkaPredmetov[3].ECTS.ToString();

  //      n5.Text = Global.zbirkaPredmetov[4].Naziv;
  //      e5.Text = Global.zbirkaPredmetov[4].ECTS.ToString();

  //      n6.Text = Global.zbirkaPredmetov[5].Naziv;
  //      e6.Text = Global.zbirkaPredmetov[5].ECTS.ToString();

  //      n7.Text = Global.zbirkaPredmetov[6].Naziv;
  //      e7.Text = Global.zbirkaPredmetov[6].ECTS.ToString();

  //      n8.Text = Global.zbirkaPredmetov[7].Naziv;
  //      e8.Text = Global.zbirkaPredmetov[7].ECTS.ToString();

  //      n9.Text = Global.zbirkaPredmetov[8].Naziv;
  //      e9.Text = Global.zbirkaPredmetov[8].ECTS.ToString();
    }
}