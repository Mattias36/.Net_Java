using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace Gui;
public partial class MainPage : ContentPage
{
    private readonly TriviaDbContext _dbContext;
    public ObservableCollection<TriviaQuestionDb> Questions { get; set; } = new ObservableCollection<TriviaQuestionDb>();

    public MainPage()
    {
        InitializeComponent();
        _dbContext = new TriviaDbContext(); 
        LoadQuestions();
        BindingContext = this;
    }

    private void LoadQuestions()
    {
        Questions.Clear();
        var questionsFromDb = _dbContext.Questions.Include(q => q.Answers).ToList();
        foreach (var question in questionsFromDb)
        {
            Questions.Add(question);
        }
        QuestionsListView.ItemsSource = Questions;
    }

    private async void FetchQuestionsFromApi(object sender, EventArgs e)
    {
      
        StatusLabel.Text = "Pobieranie danych...";
        var newQuestions = await _FetchFromApi();
        if (newQuestions.Any())
        {
            _dbContext.Questions.AddRange(newQuestions);
            await _dbContext.SaveChangesAsync();
            StatusLabel.Text = "Dane pobrane! Kliknij ponownie aby pobrać więcej";
            
            LoadQuestions();  
        }
    }

    private async Task<List<TriviaQuestionDb>> _FetchFromApi()
    {
        using HttpClient client = new HttpClient();
        string url = "https://opentdb.com/api.php?amount=5&type=multiple";

        try
        {
            string json = await client.GetStringAsync(url);
            var triviaResponse = JsonSerializer.Deserialize<TriviaResponse>(json);

            if (triviaResponse?.Questions == null || !triviaResponse.Questions.Any())
            {
                return new List<TriviaQuestionDb>();
            }

            return triviaResponse.Questions.Select(q => new TriviaQuestionDb
            {
                Question = q.Question,
                Answers = q.IncorrectAnswers
                    .Select(ans => new AnswerDb { AnswerText = ans, IsCorrect = false })
                    .Append(new AnswerDb { AnswerText = q.CorrectAnswer, IsCorrect = true })
                    .ToList()
            }).ToList();
        }
        catch
        {
            return new List<TriviaQuestionDb>();
        }
    }
    private async void OnQuestionSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is TriviaQuestionDb selectedQuestion)
        {
            await Navigation.PushAsync(new QuestionDetailPage(selectedQuestion));
        }
    }

    private async void ClearDatabase(object sender, EventArgs e)
    {
        _dbContext.Answers.RemoveRange(_dbContext.Answers);
        _dbContext.Questions.RemoveRange(_dbContext.Questions);
        await _dbContext.SaveChangesAsync();

        Questions.Clear(); 
    }
}
