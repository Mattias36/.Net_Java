using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Text.Json;
[assembly: InternalsVisibleTo("Gui")]
class Program
{
    static async Task Main()
    {
    //    using var db = new TriviaDbContext();
    //    if (!await db.Questions.AnyAsync())  // Używamy async
    //    {
    //        Console.WriteLine("Brak danych w bazie. Pobieranie z API...");
    //        List<TriviaQuestionDb> questions = await FetchFromApi();
    //        db.Questions.AddRange(questions);
    //        await db.SaveChangesAsync(); // Zmieniamy na async
    //    }

    //    Console.WriteLine("Pytania w bazie:");
    //    var questionsWithAnswers = await db.Questions.Include(q => q.Answers).ToListAsync();
    //    foreach (var question in questionsWithAnswers)
    //    {
    //        Console.WriteLine($"Pytanie: {question.Question}");
    //        foreach (var answer in question.Answers)
    //        {
    //            string marker = answer.IsCorrect ? "(correct)" : "(incorrect)";
    //            Console.WriteLine($"  {marker} {answer.AnswerText}");
    //        }
    //        Console.WriteLine();
    //    }
    //}

    //static async Task<List<TriviaQuestionDb>> FetchFromApi()
    //{
    //    using HttpClient client = new HttpClient();
    //    string url = "https://opentdb.com/api.php?amount=5&type=multiple";

    //    try
    //    {
    //        string json = await client.GetStringAsync(url);
    //        var triviaResponse = JsonSerializer.Deserialize<TriviaResponse>(json);

    //        if (triviaResponse?.Questions == null || !triviaResponse.Questions.Any())
    //        {
    //            Console.WriteLine("Błąd: API nie zwróciło pytań.");
    //            return new List<TriviaQuestionDb>();
    //        }

    //        return triviaResponse.Questions.Select(q => new TriviaQuestionDb
    //        {
    //            Question = q.Question,
    //            Answers = q.IncorrectAnswers
    //                .Select(ans => new AnswerDb { AnswerText = ans, IsCorrect = false })
    //                .Append(new AnswerDb { AnswerText = q.CorrectAnswer, IsCorrect = true })
    //                .ToList()
    //        }).ToList();
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine($"Błąd podczas pobierania danych z API: {ex.Message}");
    //        return new List<TriviaQuestionDb>();
    //    }
    }
}
