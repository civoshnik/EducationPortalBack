using Course.Application.Commands.CreateQuestion;
using Course.Domain.Models;
using MediatR;
using Shared.Application.Interfaces;

public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateQuestionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = new QuestionEntity
        {
            QuestionId = Guid.NewGuid(),
            TestId = request.TestId,
            Text = request.Text,
            Type = request.Type,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Questions.AddAsync(question, cancellationToken);

        foreach (var answerDto in request.Answers)
        {
            var answer = new QuestionAnswerEntity
            {
                QuestionAnswerId = Guid.NewGuid(),
                QuestionId = question.QuestionId,
                Text = answerDto.Text,
                IsCorrect = answerDto.IsCorrect
            };
            await _unitOfWork.QuestionAnswers.AddAsync(answer, cancellationToken);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
