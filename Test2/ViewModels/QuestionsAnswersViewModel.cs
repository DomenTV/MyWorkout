using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test2.Models;
using MySql.Data.MySqlClient;

namespace Test2.ViewModels
{
    internal class QuestionsAnswersViewModel
    {
        public List<FitnessQuestion> questionsANDanswers { get; set; }
        private INavigation _navigation;
        public string question { get; set; }
        public string answer { get; set; }
        public bool IsAnswerVisible { get; set; }
        public EventHandler OnItemSelected { get; set; }
        public object sender1 { get; set; }
        public SelectedItemChangedEventArgs e1 { get; set; }
        public EventHandler OnItemSelectedTapped { get; set; }

        //private FitnessQuestion _selectedItem;
        //public FitnessQuestion SelectedItem
        //{
        //    get => _selectedItem;
        //    set
        //    {
        //        _selectedItem = value;
        //        IsAnswerVisible = value != null;
        //    }
        //}

        public QuestionsAnswersViewModel(INavigation navigation)
        {
            this.questionsANDanswers = Global.questionsANDanswers;
            this._navigation = navigation;
            IsAnswerVisible = true;
            //OnItemSelected = new EventHandler(OnItemSelectedTapped(sender1, e1));
            //OnItemSelected += OnItemSelectedTapped(sender1, e1);
            OnItemSelectedTapped = (sender, e) => OnItemSelectedTappedMethod(sender, (SelectedItemChangedEventArgs)e);
            questionsANDanswers = Global.questionsANDanswers;
            //App.Current.MainPage.DisplayAlert("Wrong login information!", (this.questionsANDanswers.Count).ToString(), "OK");
        }
        private void OnItemSelectedTappedMethod(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                // An item was selected, set IsAnswerVisible to true
                IsAnswerVisible = true;
            }
            else
            {
                // An item was deselected, set IsAnswerVisible to false
                IsAnswerVisible = false;
            }
        }
        //    private void OnItemSelectedTapped(object sender)
        //    {
        //        App.Current.MainPage.DisplayAlert("Wrong login information!", (this.answer).ToString(), "OK");
        //    }
        //}
    }
}