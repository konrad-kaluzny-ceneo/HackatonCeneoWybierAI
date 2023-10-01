using Backend.Model;
using Newtonsoft.Json;
using OpenAI.Interfaces;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;

namespace Backend.OpenAI;

public class OpenAIQuestionProvider
{
    private readonly IOpenAIService _openAIService;

    private string SystemPrompt = """
Twoim zadaniem jest wspomóc użytkownika w wyborze studiów. Zadawaj pytania. Na podstawie tych pytań ustalisz jaki kierunek studiów będzie pasował najbardziej. Na pytania powinno dać się odpowiedzieć "tak" lub "nie". W pytaniach skup się na czynnościach, które student wykonuje w trakcie studiów.

Uwaga! Odpowiadaj tylko w formacie JSON.

Format JSON:
```
{
"Question: "[następne pytanie]"
"Proposals": [lista proponowanych kierunków]
}
```

W polu "proposed_fields" zawrzyj kierunki studiów, które możesz zaproponować na podstawie dotyczas zadanych pytań.
""";

    public OpenAIQuestionProvider(IOpenAIService openAIService)
    {
        _openAIService = openAIService;
    }

    public async Task<QuestionAndAnswer> GetNextQuestion(List<QuestionAndAnswer> questionnaireHistory, string userAnswer = "")
    {
        if (string.IsNullOrEmpty(userAnswer))
        {
            userAnswer = "Wygeneruj następne pytanie";
        }

        var chatMessages = new List<ChatMessage>
            {
                ChatMessage.FromSystem(SystemPrompt),
                ChatMessage.FromUser("Wygeneruj następne pytanie")
            };

        foreach (var historyElement in questionnaireHistory)
        {
            var sentMessage = JsonConvert.SerializeObject(new
            {
                Question = historyElement.Question,
                Proposals = historyElement.Proposals
            });

            chatMessages.Add(ChatMessage.FromAssistant(sentMessage));

            chatMessages.Add(ChatMessage.FromUser(historyElement.Answer ?? ""));
        }

        Console.WriteLine(JsonConvert.SerializeObject(chatMessages));

        var result = await _openAIService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
        {
            Messages = chatMessages,
            Model = Models.Gpt_3_5_Turbo,
            MaxTokens = 250
        });

        //return result.Choices.First().Message.Content;

        return JsonConvert.DeserializeObject<QuestionAndAnswer>(result.Choices.First().Message.Content);
    }
}
