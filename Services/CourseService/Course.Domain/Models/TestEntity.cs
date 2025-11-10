using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Domain.Models
{
    public class TestEntity
    {
        [Key]
        public Guid TestId { get; set; }
        public Guid LessonId { get; set; }
        public string Name { get; set; }
        public int QuestionCount { get; set; }
        public int AttemptRestriction { get; set; }
        public int PassingScore { get; set; }
        public int TimeLimitMinutes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
