using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Queries.GetQuestionsWithAnswersByTest
{
    public class GetQuestionsWithAnswersByTestQueryHandler
    : IRequestHandler<GetQuestionsWithAnswersByTestQuery, List<QuestionWithAnswersDto>>
    {
        private readonly IUnitOfWork _uow;
        public GetQuestionsWithAnswersByTestQueryHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<List<QuestionWithAnswersDto>> Handle(GetQuestionsWithAnswersByTestQuery request, CancellationToken ct)
        {
            var questions = await _uow.Questions
                .Where(q => q.TestId == request.TestId)
                .ToListAsync(ct);

            var questionIds = questions.Select(q => q.QuestionId).ToList();

            var answers = await _uow.QuestionAnswers
                .Where(a => questionIds.Contains(a.QuestionId))
                .ToListAsync(ct);

            var result = questions.Select(q => new QuestionWithAnswersDto
            {
                QuestionId = q.QuestionId,
                TestId = q.TestId,
                Text = q.Text,
                Type = q.Type,
                CreatedAt = q.CreatedAt,
                ModifiedAt = q.ModifiedAt,
                Answers = answers.Where(a => a.QuestionId == q.QuestionId).ToList()
            }).ToList();

            return result;
        }
    }

}
