namespace Backend.Model;

public class Academy
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string ManagingInstitution { get; set; }
    public string Level { get; set; }
    public string LaunchProfessionalTitle { get; set; }
    public string LaunchLanguageofEducation { get; set; }
    public int? LaunchNumberofSemesters { get; set; }
    public string Profile { get; set; }
    public IEnumerable<string> Disciplines { get; set; }
}