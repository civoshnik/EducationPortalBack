using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Domain.Models
{
    public class LearningProgressEntity
    {
        public Guid LessonId { get; set; }
        public Guid SubscriptionId { get; set; }
    }
}
