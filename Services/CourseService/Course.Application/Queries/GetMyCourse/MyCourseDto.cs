using Course.Domain.Models;

namespace Course.Application.Queries.GetMyCourse
{
    public class MyCourseDto
    {
        public CourseEntity Course { get; set; }
        public decimal ProgressPercent { get; set; }
    }
}
