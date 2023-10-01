using Backend.Infrastructure.Models;
using Backend.Model;
using Backend.Model.Interfaces;
using CsvHelper;
using System.Globalization;

namespace Backend.Infrastructure.Repositories
{
    public class InMemoryAcademyRepository : IAcademyRepository
    {
        private readonly List<Academy> _academyList;

        public InMemoryAcademyRepository()
        {
            using var reader = new StreamReader("kierunki-krakow-wyczyszczone.csv");
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var res = csv.GetRecords<AcademyEntity>();
            _academyList = res
                .Select(e => new Academy
                {
                    Id = e.Id,
                    ManagingInstitution = e.ManagingInstitution,
                    LaunchLanguageofEducation = e.LaunchLanguageofEducation,
                    LaunchProfessionalTitle = e.LaunchProfessionalTitle,
                    Level = e.Level,
                    Name = e.Name,
                    Disciplines = e.Disciplines.Split(",").Select(x => x.Trim()),
                    LaunchNumberofSemesters = int.TryParse(e.LaunchNumberofSemesters.Replace(".0", ""), out var val) ? val : null,
                    Profile = e.Profile
                })
                .ToList();
        }

        public Task<List<Academy>> GetAcademies(GetAcademySearchParams searchParameters)
        {
            var result = _academyList
                .Where(a => searchParameters.ProfessionalTitle is null || a.LaunchProfessionalTitle.ToLower() == searchParameters.ProfessionalTitle)
                .Where(a => searchParameters.FieldsOfStudy is null || searchParameters.FieldsOfStudy.Contains(a.Name.ToLower()))
                .Where(a => searchParameters.LanguageOfEducation is null || a.LaunchLanguageofEducation.ToLower() == searchParameters.LanguageOfEducation)
                .Where(a => searchParameters.Disciplines is null || a.Disciplines.Any(d => searchParameters.Disciplines.Any(sd => sd.Contains(d) || d.Contains(sd))))
                .Where(a => searchParameters.Profile is null || searchParameters.Profile == a.Profile.ToLower())
                .Where(a => searchParameters.LaunchNumberofSemesters is null || searchParameters.LaunchNumberofSemesters >= a.LaunchNumberofSemesters)
                .ToList();

            return Task.FromResult(result);
        }
    }
}
