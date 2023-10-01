using Backend.Model;
using Backend.Model.Interfaces;

namespace Backend.Infrastructure.Repositories
{
    public class InMemoryResutlsRepository : IResultsRepository
    {
        private Dictionary<int, Results> resultsStore = new Dictionary<int, Results>();

        public Results GetResults(int sessionId)
        {
            return resultsStore.TryGetValue(sessionId, out var result) ? result : new Results();
        }

        public int SaveResults(Results results)
        {
            int newResultId = resultsStore.Count;

            results.Id = newResultId;

            resultsStore.Add(newResultId, results);

            return newResultId;
        }
    }
}
