using Test2.ViewModels;

namespace Test2;

public partial class MainPage : ContentPage
{


    public MainPage()
    {

        var collection = Global.firebaseClient.Child("predmeti").AsObservable<Predmet>().Subscribe((item) =>
        {
            if (item.Object != null)
            {
                //DisplayAlert("IF stavek", item.Object.Naziv, "ok");
                Global.zbirkaPredmetov.Add(new Predmet(item.Object.Naziv, item.Object.PredmetID, item.Object.ECTS, item.Object.Semester));
            }
        });
        // Create the 20 fitness questions and store them in the array
        Global.arrayQuestions[0] = new FitnessQuestion { question = "What are some good exercises to get started with?", answer = "Some good exercises to get started with are bodyweight exercises such as push-ups, squats, lunges, and planks." };
        Global.arrayQuestions[1] = new FitnessQuestion { question = "What is the best way to lose weight?", answer = "The best way to lose weight is to create a calorie deficit by burning more calories than you consume. This can be achieved through a combination of diet and exercise." };
        Global.arrayQuestions[2] = new FitnessQuestion { question = "How often should I be working out?", answer = "The recommended frequency of exercise depends on your goals and your current fitness level. Generally, it is recommended to aim for at least 150 minutes of moderate intensity or 75 minutes of vigorous intensity exercise per week. It is also important to include strength training exercises at least 2 days per week." };
        Global.arrayQuestions[3] = new FitnessQuestion { question = "What should I eat before and after a workout?", answer = "It is important to fuel your body before and after a workout. Before a workout, it is a good idea to eat a snack that includes both carbohydrates and protein to give you energy and help with muscle recovery. Some good options include a banana with peanut butter or a protein bar. After a workout, it is important to replenish your glycogen stores and help with muscle recovery. Good options include a smoothie with protein powder or a bowl of whole grain cereal with milk." };
        Global.arrayQuestions[4] = new FitnessQuestion { question = "What is the best way to warm up before a workout?", answer = "Warming up before a workout is important to prepare your body for physical activity and reduce your risk of injury. A good warm-up should include dynamic stretches, which involve movement and help to loosen up the muscles, as well as some light cardio to increase your heart rate and blood flow." };
        Global.arrayQuestions[5] = new FitnessQuestion { question = "What are some good exercises for abs?", answer = "Some good exercises for abs include planks, sit-ups, Russian twists, and bicycle crunches. It is important to include a variety of exercises in your routine and to also work on strengthening your back muscles to help improve your posture." };
        Global.arrayQuestions[6] = new FitnessQuestion { question = "What is the best way to reduce cellulite?", answer = "Cellulite is caused by a combination of factors, including genetics, hormone levels, and the structure of the connective tissue in the skin. While there is no surefire way to completely eliminate cellulite, a combination of strength training exercises and cardiovascular exercise can help to reduce its appearance. Massage and dry brushing can also be helpful." };
        Global.arrayQuestions[7] = new FitnessQuestion { question = "What are some good exercises for toning my arms?", answer = "Some good exercises for toning the arms include push-ups, tricep dips, bicep curls, and overhead presses. It is important to include a variety of exercises in your routine." };
        Global.arrayQuestions[8] = new FitnessQuestion { question = "How can I improve my endurance?", answer = "To improve endurance, it is important to engage in regular cardiovascular exercise. This can include activities such as running, cycling, or swimming. It is also important to gradually increase the intensity and duration of your workouts over time." };
        Global.arrayQuestions[9] = new FitnessQuestion { question = "What is the best way to get rid of love handles?", answer = "Love handles, or excess fat around the waist, can be reduced through a combination of diet and exercise. It is important to focus on overall weight loss and to include exercises that target the oblique muscles, such as side plank variations and Russian twists." };
        Global.arrayQuestions[10] = new FitnessQuestion { question = "How can I improve my flexibility?", answer = "To improve flexibility, it is important to incorporate stretching into your fitness routine. This can include static stretches, which are held for a period of time, or dynamic stretches, which involve movement. It is also important to stretch after a workout when the muscles are warm. Stretching on a regular basis can help to improve range of motion and reduce the risk of injury." };
        Global.arrayQuestions[11] = new FitnessQuestion { question = "What is the best way to build muscle?", answer = "To build muscle, it is important to engage in strength training exercises and progressively increase the weight or resistance. It is also important to give the muscles time to rest and recover between workouts. Adequate protein intake is also essential for muscle building. Aim for at least 0.8 grams of protein per kilogram of body weight per day." };
        Global.arrayQuestions[12] = new FitnessQuestion { question = "What is the best way to get rid of belly fat?", answer = "To get rid of belly fat, it is important to focus on overall weight loss through a combination of diet and exercise. Abdominal exercises can help to tone the muscles, but they will not get rid of fat on their own. It is also important to pay attention to portion sizes and to choose foods that are high in nutrients and low in added sugars and unhealthy fats." };
        Global.arrayQuestions[13] = new FitnessQuestion { question = "What is the best way to improve my cardiovascular fitness?", answer = "To improve cardiovascular fitness, it is important to engage in regular cardiovascular exercise such as running, cycling, or swimming. It is also important to gradually increase the intensity and duration of your workouts over time. It is recommended to aim for at least 150 minutes of moderate intensity or 75 minutes of vigorous intensity exercise per week." };
        Global.arrayQuestions[14] = new FitnessQuestion { question = "How can I prevent injuries when working out?", answer = "To prevent injuries when working out, it is important to warm up properly before exercising, to use proper form when performing exercises, and to listen to your body and rest when you are tired or in pain. It is also a good idea to vary your workouts to reduce the risk of overuse injuries. Wearing proper footwear and using the appropriate equipment can also help to prevent injuries." };
        Global.arrayQuestions[15] = new FitnessQuestion { question = "What is the best way to incorporate strength training into my routine?", answer = "To incorporate strength training into your fitness routine, it is important to choose exercises that target all of the major muscle groups and to progressively increase the weight or resistance as you get stronger." };
        Global.arrayQuestions[16] = new FitnessQuestion { question = "What is the best way to incorporate cardio into my routine?", answer = "To incorporate cardio into your fitness routine, it is important to choose activities that you enjoy and that you can sustain over the long term. Some options include running, cycling, swimming, or dancing. It is also important to gradually increase the intensity and duration of your workouts over time. It is recommended to aim for at least 150 minutes of moderate intensity or 75 minutes of vigorous intensity exercise per week." };
        Global.arrayQuestions[17] = new FitnessQuestion { question = "How can I stay motivated to work out?", answer = "To stay motivated to work out, it can be helpful to set specific and achievable goals, to track your progress, and to reward yourself when you reach your goals. It is also important to choose activities that you enjoy and to vary your workouts to keep things interesting. Having a workout buddy or joining a group fitness class can also provide accountability and support." };
        Global.arrayQuestions[18] = new FitnessQuestion { question = "What is the best way to get started with a new fitness routine?", answer = "To get started with a new fitness routine, it is important to set specific and achievable goals and to start slowly and gradually build up your intensity and duration. It is also important to choose activities that you enjoy and to vary your workouts to keep things interesting. It can be helpful to seek the guidance of a fitness professional, such as a personal trainer or exercise physiologist, to help you develop a safe and effective workout plan." };
        Global.arrayQuestions[19] = new FitnessQuestion { question = "What are some good exercises for beginners?", answer = "Some good exercises for beginners include bodyweight exercises such as push-ups, squats, lunges, and planks. These exercises can be done with minimal equipment and are a great way to build strength and improve your fitness level. It is also important to start slowly and gradually build up your intensity and duration." };
        foreach (FitnessQuestion question in Global.arrayQuestions)
        {
            Global.questionsANDanswers.Add(question);
        }

        InitializeComponent();

        //login.Text = Global.zbirkaPredmetov[0].Naziv;
        Application.Current.UserAppTheme = AppTheme.Dark;
        BindingContext = new
        LoginViewModel(Navigation);
    }

    //int count = 0;

    private void OnLoginClicked(object sender, EventArgs e)
    {
        //if (NameInput.Text == "admin" && PassInput.Text == "admin")
        //{
        //    ErrorLabel.Text = "";
        //    count = 0;
        //    Navigation.PushAsync(new Syllabus_page());
        //}
        //else
        //{
        //    count++;
        //    ErrorLabel.Text = "Wrong login information! #" + count.ToString();
        //}
        //DisplayAlert("THIS WORKS", "this as well", "jeff");
        
    }


    private void OnAboutClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new About());


    }
}

