namespace Backend.Model
{
    public class GetAcademySearchParams
    {
        public List<string> FieldsOfStudy { get; set; }
        public string ProfessionalTitle { get; set; }
        public string LanguageOfEducation { get; set; }
        public string Profile { get; set; }
        public int? LaunchNumberofSemesters { get; set; }
        public List<string> Disciplines { get; set; }
    }
}
