using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.OpenAI.QuestionProviderStructures
{
    public class QuestionAndAnswer
    {
        public required string Question { get; set; }
        public required string[] Proposals { get; set; }
        public string? Answer { get; set; }
    }
}
