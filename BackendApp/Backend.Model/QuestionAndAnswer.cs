namespace Backend.Model;

public class QuestionAndAnswer
{
    public int Id { get; set; }
    public required string Question { get; set; }
    public required string[] Proposals { get; set; }
    public string Answer { get; set; }
}
