using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Commands.SubmitTest
{
    public record SubmitTestCommand(Guid TestId, Guid UserId, List<AnswerDto> Answers, int TimeSpentSeconds) : IRequest<SubmitResult>;
}
