using Backend.Model;
using Backend.Model.Interfaces;
using Google.Cloud.Firestore;
using iHome.Microservices.Devices.Infrastructure.Logic;
using Newtonsoft.Json;
using System.Text.Json;

namespace Backend.Infrastructure.Repositories;

public class FirestoreAcademyRepository : FirestoreBaseRepository, IAcademyRepository
{
    public FirestoreAcademyRepository(IFirestoreConnectionFactory firestoreDb)
        : base(firestoreDb, "kierunki_studiow")
    {
    }

    public async Task<List<Academy>> GetAcademies(GetAcademySearchParams searchParameters)
    {
        var result = new List<Academy>();

        var collection = await GetCollection();

        var snap = await FilterDocuments(collection, searchParameters).GetSnapshotAsync();

        foreach (var snapshot in snap.Documents)
        {
            var dictionary = JsonConvert.SerializeObject(snapshot.ToDictionary());
            var r = JsonConvert.DeserializeObject<Academy>(dictionary);

            result.Add(r);
        }

        return result;
    }

    private Query FilterDocuments(CollectionReference collectionReference, GetAcademySearchParams searchParameters)
    {
        return collectionReference
            .WhereEqualTo("LaunchProfessionalTitle", searchParameters.ProfessionalTitle)
            .WhereIn("Name", searchParameters.FieldsOfStudy)
            .WhereArrayContains("Disciplines", searchParameters.Disciplines)
            .WhereEqualTo("LaunchLanguageofEducation", searchParameters.LanguageOfEducation);
    }
}