using Backend.Infrastructure.Models;
using Google.Cloud.Firestore;
using Newtonsoft.Json;

namespace iHome.Microservices.Devices.Infrastructure.Logic;

public interface IFirestoreConnectionFactory
{
    Task<FirestoreDb> GetFirestoreConnection();
}

public class FirestoreConnectionFactory : IFirestoreConnectionFactory
{
    private readonly string _projectId;
    private readonly string _jsonCredentials;

    public FirestoreConnectionFactory(FirestoreOptions options)
    {
        _projectId = options.ProjectId;
        _jsonCredentials = File.ReadAllText("service.json"); // "\"{\\\"type\\\":\\\"service_account\\\",\\\"project_id\\\":\\\"hackyeahapi\\\",\\\"private_key_id\\\":\\\"4526cf5e2014b74497744b830ab3cf0d6df879f1\\\",\\\"private_key\\\":\\\"-----BEGINPRIVATEKEY-----\\nMIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQCTegODsDX0kAa4\\nflaBsdjITXvYYJroh8z1I05w+tgcANl3azzJLI4tqF8kp/okCYBns1Oll8AsqXgh\\nxyg/LrMdS0F2uF0MGVtmmcU31MBoEcd0/DI41r+FK3WMLCMG6cQIWPTBTtKxe7qQ\\n7XTK3mwYIfAi5V+fTp9jF7CaP33PsCUg4RWQ6uvmCmSaPKFYeK2RXUV04ZTfqhlF\\nMnjtvmjWRl505iAtJTSJNiB1++wSo1fTWoWs8JSZT+loLHYyIQGBrZwmK6vo4TfN\\nt7R+zSG7WW79fx2Y38a4/bb8PrEfQcg18rNFKQ+xnbogQmyPxHXSv0aQPR/DKjGg\\nsrWkqnuPAgMBAAECggEAAOBhQ/eYon72o0MQ9Y1eifY37bXJReeEttJCZZaAEU6J\\n/FU1U8bBg5D0G84pxOwPiM4+iYXo3PsW3TCHUzTOWLa5SWDYILnH2sjALNBFYnj0\\nlAPpsn/IPJzhAnfCr4hVI22P6tQymV5wYesfXhNLVithd3uggVxgwyCBarFE6KaT\\niotGECyrcTPgA14AmIibGA8fXyxHKwiR91UHt+wHDbHhfkd33gcz2u6upat9eYNO\\nKQOpOv6KQ/mdIjE/Js4PPCurSpWZWk/P/wC84dblsh7LxH793wurd5/j3CeyveGy\\ncarMVa43dlFS9gt4caZhORcDV/NiaWUNK/OjldiTOQKBgQDGEwwzk+OwKtkaNLFm\\nv4rxbVBByR5mKRpd5awtq9t8y6KRxCLrrpDvWroagTIvSRRxtKfTtbDSD/RkR1Je\\nsMHswttzM5lGqqhGg+5UBJdJ8/H4JAH9bK1D9NoJ7k0naM/FPJe+UtBK4jFSkID1\\n+XnZL/BFkflVrLxQGQ0hgNBcOQKBgQC+mu8aN7MHPCJcT9gm2y27tCzvwlbXGAHD\\n+0vD5Q1UuitmDcoCi62msekTMs7mhVk4EQNgXUYpYvzN4dWX9uGE/AIqGG+aNeXt\\nIy7/e1dW8iGKYA4jGxTtDDPJ8aS1CfTrZvUU1mE4IrTZBhsvhk37TOHHgeToI0UW\\nb8gPYoOmBwKBgQCRuLu2SmwtVCiq3e9Rz0NSQQDVlTgXItAyGmFkrFXq5wmwQeML\\nDz+zyES4cSpRnWs8CBOcbsQqlvBOwiX4YgQZwnWeuGxgj/cDAdbZ2xLOpnjy/NK1\\n9jk2kLHEspvyjWqmCeD9dYGmRejRfFxUGnkpbtpO5IjSHiXgq718U393CQKBgQCu\\nMX5PIZRWClkEsFvEtw6GutVOkPc4QFJsv56wewbB5hp/fB6gUPL0oyd1SjvYZQny\\nuWmyicvzSunrZncEGLZmCMIZopdsAdIN9Neg2SIq7cJFah/BaaCoOzyhVFvIsD7L\\nXB6jWoEjAmTw6imyNzXRcqPs5wNCO+mJ5cKafNf/xQKBgArq/IQg4K2gH5r8z0Wh\\n1hTDmUE2jS34Cu8D2cCqxJ75wu/ZWZB/DFcAs9MbMvckPPNqyFNq6WmiUEdkrl5o\\nTjWLPj3Whav9lBObK2rVryplCmEBJLy9buaHAMzthwCNF4x7DvViLeduqYJdsaog\\nIIiWao2ijZGPE0UqQopUJzuy\\n-----ENDPRIVATEKEY-----\\n\\\",\\\"client_email\\\":\\\"firebase-adminsdk-3irfi@hackyeahapi.iam.gserviceaccount.com\\\",\\\"client_id\\\":\\\"113706159009727558187\\\",\\\"auth_uri\\\":\\\"https://accounts.google.com/o/oauth2/auth\\\",\\\"token_uri\\\":\\\"https://oauth2.googleapis.com/token\\\",\\\"auth_provider_x509_cert_url\\\":\\\"https://www.googleapis.com/oauth2/v1/certs\\\",\\\"client_x509_cert_url\\\":\\\"https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-3irfi%40hackyeahapi.iam.gserviceaccount.com\\\",\\\"universe_domain\\\":\\\"googleapis.com\\\"}\"";
    }

    public async Task<FirestoreDb> GetFirestoreConnection()
    {
        var db = await new FirestoreDbBuilder
        {
            ProjectId = _projectId,
            JsonCredentials = _jsonCredentials
        }.BuildAsync();

        return db;
    }
}