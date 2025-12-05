using Enrollment.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Commands.UpdateProgress
{
    public record UpdateProgressCommand : IRequest<SubscriptionEntity>
    {
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public decimal Progress { get; set; }
    }
}
