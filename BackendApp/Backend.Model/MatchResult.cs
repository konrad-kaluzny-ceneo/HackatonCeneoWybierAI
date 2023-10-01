namespace Backend.Model;

public class MatchResult
{
    public string Name { get; set; }
    public decimal PercentageMatch { get; set; }
    public List<string> ManagingInstitutions { get; set; }
}
