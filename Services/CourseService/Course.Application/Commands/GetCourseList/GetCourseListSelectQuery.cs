using Course.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Commands.GetCourseList
{
    public record GetCourseListSelectQuery : IRequest<List<CourseEntity>>
    {
    }
}
