using Course.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Queries.GetQuestionsWithAnswersByTest
{
    public class QuestionWithAnswersDto
    {
        public Guid QuestionId { get; set; }
        public Guid TestId { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public List<QuestionAnswerEntity> Answers { get; set; } = new();
    }

}
