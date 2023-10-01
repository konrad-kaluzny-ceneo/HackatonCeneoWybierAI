using Backend.Model;
using Backend.Model.Interfaces;
using Newtonsoft.Json;
using OpenAI.Interfaces;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;

namespace Backend.OpenAI;

public class OpenAISearchParamsGenerator : ISearchParamsGenerator
{
    private readonly IOpenAIService _openAIService;

    public OpenAISearchParamsGenerator(IOpenAIService openAIService)
    {
        _openAIService = openAIService;
    }

    public async Task<GetAcademySearchParams> Generate(List<QuestionWithAnswer> questions)
    {
        var result = await _openAIService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
        {
            Messages = new List<ChatMessage>
            {
                ChatMessage.FromSystem(@"
Respond only with JSON.
Important: do not return anything but JSON!

Twoim zadaniem jest zaproponowanie kierunku studiów użytkownikowi. Twoja wiedza o użytkowniku opera się na wypełnionej przez niego ankiecie. O to wyniki ankiety:
[TREŚĆ USERA]

Propozycję zwróć w formie danych JSON. Wartości pól powinny być napisane po polsku z małej litery. Dane JSON powinny być zwrócone w takim formacie:

{
  ""fieldsOfStudy"" - lista proponowanych kierunków,
 ""LaunchForm"": ""stacjonarne"" albo ""zaoczne"",
 ""Profile"": ""ogólnoakademicki"" albo ""praktyczny"",
 ""LaunchNumberofSemesters"": 12
}
                    "),
                ChatMessage.FromUser(JsonConvert.SerializeObject(questions))
            },
            Model = Models.Gpt_4,
            MaxTokens = 250
        });

        var content = result.Choices.First().Message.Content;

        try
        {
            var parameters = JsonConvert.DeserializeObject<GetAcademySearchParams>(content);
            if (parameters is null)
            {
                return new GetAcademySearchParams();
            }

            return parameters;
        }
        catch (Exception)
        {
            return new GetAcademySearchParams();
        }
    }

    public async Task<GetAcademySearchParams> Generate(List<QuestionAndAnswer> questions)
    {
        var result = await _openAIService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
        {
            Messages = new List<ChatMessage>
            {
                ChatMessage.FromSystem(@"
Respond only with JSON.
Important: do not return anything but JSON!

Twoim zadaniem jest zaproponowanie kierunku studiów użytkownikowi. Twoja wiedza o użytkowniku opera się na wypełnionej przez niego ankiecie. O to wyniki ankiety:
[TREŚĆ USERA]

Propozycję zwróć w formie danych JSON. Wartości pól powinny być napisane po polsku z małej litery. Dane JSON powinny być zwrócone w takim formacie:

{
  ""fieldsOfStudy"" - lista proponowanych kierunków,
 ""LaunchForm"": ""stacjonarne"" albo ""zaoczne"",
 ""Profile"": ""ogólnoakademicki"" albo ""praktyczny"",
 ""LaunchNumberofSemesters"": 12
}
                    "),
                ChatMessage.FromUser(JsonConvert.SerializeObject(questions))
            },
            Model = Models.Gpt_4,
            MaxTokens = 250
        });

        var content = result.Choices.First().Message.Content;

        try
        {
            var parameters = JsonConvert.DeserializeObject<GetAcademySearchParams>(content);
            if (parameters is null)
            {
                return new GetAcademySearchParams();
            }

            return parameters;
        }
        catch (Exception)
        {
            return new GetAcademySearchParams();
        }
    }
}