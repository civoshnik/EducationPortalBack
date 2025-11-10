using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Commands.CreateCourse
{
    public record CreateCourseCommand : IRequest
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Level { get; set; }
        public int DurationHours { get; set; }
        public string Creator { get; set; }
        public bool IsPublished { get; set; }
    }
}
