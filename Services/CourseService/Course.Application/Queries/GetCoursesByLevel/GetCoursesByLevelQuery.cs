using Course.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Queries.GetCoursesByLevel
{
    public record GetCoursesByLevelQuery(string Level) : IRequest<IEnumerable<CourseEntity>>
    {
    }
}
