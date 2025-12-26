using Course.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Queries.GetQuestionsByTest
{

    public class GetQuestionsByTestQueryHandler : IRequestHandler<GetQuestionsByTestQuery, List<QuestionEntity>>
    {
        private readonly IUnitOfWork _uow;
        public GetQuestionsByTestQueryHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<List<QuestionEntity>> Handle(GetQuestionsByTestQuery request, CancellationToken ct)
        {
            return await _uow.Questions
                .Where(q => q.TestId == request.TestId)
                .ToListAsync(ct);
        }
    }


}
