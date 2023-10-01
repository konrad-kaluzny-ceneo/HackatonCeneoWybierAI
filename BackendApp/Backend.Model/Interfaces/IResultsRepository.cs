namespace Backend.Model.Interfaces;

public interface IResultsRepository
{
    Results? GetResults(int sessionId);
    int SaveResults(Results results);
}
