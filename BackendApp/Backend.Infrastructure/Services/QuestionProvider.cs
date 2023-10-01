using Backend.Model.QuizOutput;

namespace Backend.Infrastructure.Services
{
    public class QuestionProvider : IQuestionsProvider
    {
        Question[] careerAssessmentQuestions = new Question[10]
        {
            new Question { Id = 1, Value = "Czy lubisz rozwiązywać skomplikowane problemy matematyczne lub logiczne?" },
            new Question { Id = 2, Value = "Czy interesuje Cię biologia i/lub chemia?"  },
            new Question { Id = 3, Value = "Czy chętnie pracujesz z komputerem i programujesz?"  },
            new Question { Id = 4, Value = "Czy lubisz pomagać innym ludziom i jesteś empatyczny?" },
            new Question { Id = 5, Value = "Czy interesuje Cię historia i kultura różnych krajów?" },
            new Question { Id = 6, Value = "Czy prawo i sprawy społeczne są dla Ciebie interesujące?"  },
            new Question { Id = 7, Value = "Czy czerpiesz przyjemność z tworzenia czegoś nowego, np. sztuki, muzyki, projektów?" },
            new Question { Id = 8, Value = "Czy ekonomia, biznes lub zarządzanie są dla Ciebie interesujące?" },
            new Question { Id = 9, Value = "Czy lubisz pracować z danymi i analizować je?" },
            new Question { Id = 10, Value = "Czy nauki ścisłe, takie jak fizyka czy astronomia, są dla Ciebie interesujące?" }
        };

        public Question[] GetQuestions()
        {
            return careerAssessmentQuestions;
        }
    }
}
