namespace Backend.Model.Interfaces;

public interface IAcademyRepository
{
    Task<List<Academy>> GetAcademies(GetAcademySearchParams searchParameters);
}
