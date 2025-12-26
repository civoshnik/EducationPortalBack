using Course.Application.Queries.GetPaginatedLessonList;
using Course.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;
using Shared.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Queries.GetPaginatedQuestionList
{
    public class GetPaginatedQuestionListQueryHandler : IRequestHandler<GetPaginatedQuestionListQuery, PaginatedResult<QuestionEntity>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPaginatedQuestionListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResult<QuestionEntity>> Handle(GetPaginatedQuestionListQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.Questions.OrderByDescending(c => c.CreatedAt);

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedResult<QuestionEntity>
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }
}
