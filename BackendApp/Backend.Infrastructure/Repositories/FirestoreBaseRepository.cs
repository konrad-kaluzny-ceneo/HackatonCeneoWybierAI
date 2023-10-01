using Google.Cloud.Firestore;
using iHome.Microservices.Devices.Infrastructure.Logic;

namespace Backend.Infrastructure.Repositories
{
    public class FirestoreBaseRepository
    {
        private readonly IFirestoreConnectionFactory _factory;
        private readonly string _collectionName;

        public FirestoreBaseRepository(IFirestoreConnectionFactory factory, string collectionName)
        {
            _factory = factory;
            _collectionName = collectionName;
        }


        protected Task<FirestoreDb> FirestoreDb => _factory.GetFirestoreConnection();

        protected async Task<CollectionReference> GetCollection()
        {
            var client = await FirestoreDb;

            return client.Collection(_collectionName);
        }
    }
}
