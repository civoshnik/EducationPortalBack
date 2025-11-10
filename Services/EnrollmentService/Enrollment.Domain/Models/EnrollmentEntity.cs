using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enrollment.Domain.Models
{
    public class EnrollmentEntity
    {
        [Key]
        public Guid EnrollmentId { get; set; }
        public Guid CourseId { get; set; }
        public Guid UserId { get; set; }
        public string Status { get; set; }
        public decimal ProgressPercent { get; set; }
        public DateTime EnrolledAt { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
