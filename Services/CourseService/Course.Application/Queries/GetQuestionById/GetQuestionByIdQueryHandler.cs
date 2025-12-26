using Course.Application.Queries.GetQuestionById;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;

public class GetQuestionByIdQueryHandler : IRequestHandler<GetQuestionByIdQuery, QuestionWithAnswersDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetQuestionByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new Exception(nameof(unitOfWork));
    }

    public async Task<QuestionWithAnswersDto> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
    {
        var targetQuestion = await _unitOfWork.Questions.SingleOrDefaultAsync(q => q.QuestionId == request.QuestionId, cancellationToken);

        if (targetQuestion == null)
            return null;

        var answers = await _unitOfWork.QuestionAnswers
            .Where(a => a.QuestionId == request.QuestionId)
            .ToListAsync(cancellationToken);

        return new QuestionWithAnswersDto
        {
            QuestionId = targetQuestion.QuestionId,
            TestId = targetQuestion.TestId,
            Text = targetQuestion.Text,
            Type = targetQuestion.Type,
            CreatedAt = targetQuestion.CreatedAt,
            ModifiedAt = targetQuestion.ModifiedAt,
            Answers = answers
        };
    }
}
