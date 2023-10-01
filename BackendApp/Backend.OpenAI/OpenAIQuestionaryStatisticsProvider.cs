using Backend.Model;
using Backend.Model.Interfaces;
using Newtonsoft.Json;
using OpenAI.Interfaces;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;

namespace Backend.OpenAI;

internal class OpenAIQuestionaryStatisticsProvider : IQuestionaryStatisticsProvider
{
    private readonly IOpenAIService _openAIService;

    public OpenAIQuestionaryStatisticsProvider(IOpenAIService openAIService)
    {
        _openAIService = openAIService;
    }

    public async Task<string> GetExpertDescription(List<Academy> academies)
    {
        var result = await _openAIService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
        {
            Messages = new List<ChatMessage>
            {
                ChatMessage.FromSystem("Proszę zwróć wynik w formie krótkiego podsumowania. Zwracaj się bezpośrednio do użytkownika. Nie zwracaj odpowiedzi w formie listy. UWAGA!!! Ogranicz się do 200 słów."),
                ChatMessage.FromUser(@"
Twoim celem jest pokierownaie użytkownika w wyborze studiów. Ponieżej znajduje się lista uczelni które chcesz mu zaproponować, proszę napisz krótkie podsumowanie."),
                ChatMessage.FromUser(JsonConvert.SerializeObject(academies))
            },
            Model = Models.Gpt_3_5_Turbo,
            MaxTokens = 250
        });

        return result.Choices?.FirstOrDefault()?.Message?.Content ?? string.Empty;
    }

    public async Task<Dictionary<string, decimal>> GetMatchRates(List<QuestionWithAnswer> questionnaireHistory, List<string> fieldsOfStudies)
    {
        var result = await _openAIService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
        {
            Messages = new List<ChatMessage>
            {
                ChatMessage.FromSystem(@"
Proszę zwróć wynik w listy par wartości nazwa, dopasowanie na podstawie podanych pytań oraz wybranych kierunków w formacie JSON. Proszę ładnie zwróć dane z małej litery.

Format JSON:
{
""[nazwa kierunki]: ""[procent dopasowania]""
}

Dane od użytkownika:
[odpowiedzi]
[kierunki studiów]
"),
                ChatMessage.FromUser(JsonConvert.SerializeObject(questionnaireHistory)),
                ChatMessage.FromUser(JsonConvert.SerializeObject(fieldsOfStudies))
            },
            Model = Models.Gpt_4,
            MaxTokens = 250
        });

        var content = result.Choices?.FirstOrDefault()?.Message?.Content?.Replace("%", "");

        try
        {
            if (content is null) return new Dictionary<string, decimal>();

            var matches = JsonConvert.DeserializeObject<Dictionary<string, decimal>>(content);

            if (matches is null) return new Dictionary<string, decimal>();

            return matches;
        }
        catch (Exception)
        {
            return new Dictionary<string, decimal>();
        }

    }

    public async Task<Dictionary<string, decimal>> GetMatchRates(List<QuestionAndAnswer> questions, List<string> fieldsOfStudies)
    {
        var result = await _openAIService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
        {
            Messages = new List<ChatMessage>
            {
                ChatMessage.FromSystem(@"
Proszę zwróć wynik w listy par wartości nazwa, dopasowanie na podstawie podanych pytań oraz wybranych kierunków w formacie JSON. Proszę ładnie zwróć dane z małej litery.

Format JSON:
{
""[nazwa kierunki]: ""[procent dopasowania]""
}

Dane od użytkownika:
[odpowiedzi]
[kierunki studiów]
"),
                ChatMessage.FromUser(JsonConvert.SerializeObject(questions)),
                ChatMessage.FromUser(JsonConvert.SerializeObject(fieldsOfStudies))
            },
            Model = Models.Gpt_4,
            MaxTokens = 250
        });

        var content = result.Choices?.FirstOrDefault()?.Message?.Content?.Replace("%", "");

        try
        {
            if (content is null) return new Dictionary<string, decimal>();

            var matches = JsonConvert.DeserializeObject<Dictionary<string, decimal>>(content);

            if (matches is null) return new Dictionary<string, decimal>();

            return matches;
        }
        catch (Exception)
        {
            return new Dictionary<string, decimal>();
        }
    }
}
