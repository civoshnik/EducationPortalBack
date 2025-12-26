using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Queries.GetQuestionsWithAnswersByTest
{
    public record GetQuestionsWithAnswersByTestQuery(Guid TestId) : IRequest<List<QuestionWithAnswersDto>>;
}
