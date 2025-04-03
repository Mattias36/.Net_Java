using Microsoft.Maui.Controls;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Gui
{
    public partial class QuestionDetailPage : ContentPage
    {
        private readonly TriviaQuestionDb _question;
        private TriviaDbContext _dbContext;

        public QuestionDetailPage(TriviaQuestionDb question)
        {
            InitializeComponent();
            _question = question;
            _dbContext = new TriviaDbContext();
            LoadQuestionDetails();
        }

        private async void LoadQuestionDetails()
        {
            QuestionLabel.Text = _question.Question;

            var answers = await _dbContext.Answers
                .Where(a => a.TriviaQuestionDbId == _question.Id)
                .ToListAsync();

            AnswersListView.ItemsSource = answers;
        }
    }
}
