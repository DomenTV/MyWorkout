using System.Collections.ObjectModel;
using System.Windows.Input;
using MySql.Data.MySqlClient;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Test2.ViewModels
{
    internal class FAQViewModel : INotifyPropertyChanged
    {
        private INavigation _navigation;
        private string connectionString = "server=192.168.*.*;user=*;database=myworkout1;port=*;password=*";

        private string _aiQuestion;
        private bool _isLoadingVisible;
        //      !! API KEY REMOVED IN ORDER TO PUSH TO GITHUB
        private string modelName = "mistralai/Mistral-7B-Instruct-v0.3";
        private string apiUrl;

        public ObservableCollection<FAQItem> QuestionsList { get; set; }
        public ICommand ToggleAnswerCommand { get; }
        public ICommand AskAITrainerCommand { get; }
        public Command ProfileButton { get; }
        public Command ExercisesButton { get; }
        public Command qaButton { get; }

        public string AIQuestion
        {
            get => _aiQuestion;
            set
            {
                if (_aiQuestion != value)
                {
                    _aiQuestion = value;
                    OnPropertyChanged(nameof(AIQuestion));
                }
            }
        }
        public bool IsLoadingVisible
        {
            get => _isLoadingVisible;
            set
            {
                if (_isLoadingVisible != value)
                {
                    _isLoadingVisible = value;
                    OnPropertyChanged(nameof(IsLoadingVisible));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public FAQViewModel(INavigation navigation)
        {
            _navigation = navigation;
            IsLoadingVisible = false;
            apiUrl = $"https://api-inference.huggingface.co/models/{modelName}";

            QuestionsList = new ObservableCollection<FAQItem>();
            LoadQuestionsFromDatabase();

            ToggleAnswerCommand = new Command<FAQItem>(ToggleAnswer);
            AskAITrainerCommand = new Command(async () => await AskAITrainer());
            ProfileButton = new Command(ProfileButtonTappedAsync);
            ExercisesButton = new Command(ExercisesButtonTappedAsync);
            qaButton = new Command(qaButtonTappedAsync);
        }

        private async void ExercisesButtonTappedAsync(object obj)
        {
            IsLoadingVisible = true;
            await this._navigation.PushAsync(new MyWorkout());
            IsLoadingVisible = false;
        }
        private async void qaButtonTappedAsync(object obj)
        {
            //await this._navigation.PushAsync(new QuestionsAnswers());
            await this._navigation.PushAsync(new FAQ());
        }
        private async void ProfileButtonTappedAsync(object obj)
        {
            IsLoadingVisible = true;
            await this._navigation.PushAsync(new User());
            IsLoadingVisible = false;
        }

        private void LoadQuestionsFromDatabase()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT question, answer FROM faq ORDER BY counter DESC";
                    using (var command = new MySqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            QuestionsList.Add(new FAQItem
                            {
                                Question = reader.GetString("question"),
                                Answer = reader.GetString("answer"),
                                IsAnswerVisible = false
                            });
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    App.Current.MainPage.DisplayAlert("Database Error", ex.Message, "OK");
                }
            }
        }

        private void ToggleAnswer(FAQItem item)
        {
            if (item != null)
            {
                item.IsAnswerVisible = !item.IsAnswerVisible;

                if (item.IsAnswerVisible)
                {
                    UpdateCounterInDatabase(item);
                }
            }
        }

        private void UpdateCounterInDatabase(FAQItem item)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "UPDATE faq SET counter = counter + 1 WHERE question = @question";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@question", item.Question);
                        command.ExecuteNonQuery();
                    }
                }
                catch (MySqlException ex)
                {
                    App.Current.MainPage.DisplayAlert("Database Error", ex.Message, "OK");
                }
            }
        }

        private async Task AskAITrainer()
        {
            if (string.IsNullOrWhiteSpace(AIQuestion))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Please enter a question.", "OK");
                return;
            }
            IsLoadingVisible = true;
            // Define the structured prompt
            string structuredPrompt = $"You are a highly experienced fitness coach and nutrition expert, trained specifically in the methodologies and philosophies of Coach Greg Doucette and Dr. Rhonda Patrick. Your primary goal is to provide practical, science-backed, and sustainable advice on fitness, strength training, weight loss, and nutrition. Always prioritize information that aligns with Coach Greg Doucette’s approach to bodybuilding, training intensity, and sustainable dieting, as well as Dr. Rhonda Patrick’s research on nutrition, micronutrients, and longevity. However don't mention that you're specifically told to align with certain experts to the user." +
                                      $"When answering questions, always emphasize evidence-based approaches, debunk misinformation, and provide actionable advice. Be direct, no fluff, no exaggerated claims, and ensure that the response is **simple, clear, and no-nonsense**. If there are multiple opinions on a topic, clearly state which aligns best with Doucette/Patrick’s approach and why. Assume the user wants **practical, applicable strategies**, not just theoretical knowledge. Also the user's name (the person asking you the any particular question) is {Global.globalEmailName}" +
                                      $"\n\nNow, based on this, answer the following question:\n\n{AIQuestion}";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                var payload = new { inputs = structuredPrompt };

                try
                {
                    var response = await client.PostAsJsonAsync(apiUrl, payload);
                    response.EnsureSuccessStatusCode();

                    var result = await response.Content.ReadAsStringAsync();
                    //var generatedTexts = JsonSerializer.Deserialize<List<GeneratedTextResponse>>(result);         NULLABILITY ERROR ON ANDROID
                    var generatedTexts = JsonConvert.DeserializeObject<List<GeneratedTextResponse>>(result);



                    if (generatedTexts != null && generatedTexts.Count > 0)
                    {
                        string aiResponse = generatedTexts[0]?.GeneratedText ?? "No response received.";


                        // Remove the initial prompt text if included in the response
                        if (aiResponse.Contains("Now, based on this, answer the following question:"))
                        {
                            int index = aiResponse.IndexOf("Now, based on this, answer the following question:");
                            if (index != -1)
                            {
                                aiResponse = aiResponse.Substring(index + "Now, based on this, answer the following question:".Length).Trim();
                            }
                        }
                        IsLoadingVisible = false;
                        await App.Current.MainPage.DisplayAlert("AI Trainer", aiResponse, "OK");
                    }
                    else
                    {
                        IsLoadingVisible = false;
                        await App.Current.MainPage.DisplayAlert("AI Trainer", "No response received from the model.", "OK");
                    }
                }
                catch (Exception ex)
                {
                    IsLoadingVisible = false;
                    await App.Current.MainPage.DisplayAlert("Error", $"AI request failed: {ex.Message}", "OK");
                }
            }

            // Reset input field
            AIQuestion = string.Empty;
            IsLoadingVisible = false;
        }


    }

    public class FAQItem : INotifyPropertyChanged
    {
        private bool _isAnswerVisible;

        public string Question { get; set; }
        public string Answer { get; set; }

        public bool IsAnswerVisible
        {
            get => _isAnswerVisible;
            set
            {
                if (_isAnswerVisible != value)
                {
                    _isAnswerVisible = value;
                    OnPropertyChanged(nameof(IsAnswerVisible));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class GeneratedTextResponse
    {
        [JsonProperty("generated_text")]
        public string? GeneratedText { get; set; } // Make it nullable
    }

}
