using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class AnswerDb
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string AnswerText { get; set; }
    public bool IsCorrect { get; set; }  

    // Klucz obcy 
    public int TriviaQuestionDbId { get; set; }
    public TriviaQuestionDb TriviaQuestion { get; set; }
}
