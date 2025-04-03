using System.Text.Json.Serialization;

public class TriviaResponse
{
    [JsonPropertyName("results")]
    public List<TriviaQuestion> Questions { get; set; }
}

public class TriviaQuestion
{
    [JsonPropertyName("question")]
    public string Question { get; set; }

    [JsonPropertyName("correct_answer")]
    public string CorrectAnswer { get; set; }

    [JsonPropertyName("incorrect_answers")]
    public List<string> IncorrectAnswers { get; set; }
}


//podpunkt 3 dontet 
//można użyć innej operacji niż ta co w instrukcji, na przykład SEARCH