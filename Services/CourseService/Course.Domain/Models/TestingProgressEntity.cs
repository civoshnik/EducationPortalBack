using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Domain.Models
{
    public class TestingProgressEntity
    {
        public Guid TestingProgressId { get; set; }
        public Guid TestId { get; set; }
        public Guid SubscriptionId { get; set; }
        public int Grade { get; set; }
        public int AssignedGrade { get; set; }
        public int TimeSpentSeconds { get; set; }
        public DateTime PassedAt { get; set; }
    }
}
