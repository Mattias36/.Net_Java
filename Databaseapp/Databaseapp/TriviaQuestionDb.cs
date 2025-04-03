using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TriviaQuestionDb
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Question { get; set; }

    public List<AnswerDb> Answers { get; set; } = new List<AnswerDb>();
}
