using Enrollment.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Commands.EnrollCourse
{
    public record EnrollCourseCommand(Guid UserId, Guid CourseId) : IRequest<SubscriptionEntity>;
}
