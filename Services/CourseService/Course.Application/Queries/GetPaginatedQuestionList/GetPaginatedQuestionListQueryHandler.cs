using Course.Application.Queries.GetPaginatedLessonList;
using Course.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Queries.GetPaginatedQuestionList
{
    public class GetPaginatedQuestionListQueryHandler : IRequestHandler<GetPaginatedQuestionListQuery, List<QuestionEntity>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPaginatedQuestionListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<QuestionEntity>> Handle(GetPaginatedQuestionListQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Questions
                .OrderByDescending(c => c.CreatedAt)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);
        }
    }
}
