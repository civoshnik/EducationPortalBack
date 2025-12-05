using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enrollment.Domain.Models
{
    public class SubscriptionEntity
    {
        public Guid SubscriptionId { get; set; }
        public Guid CourseId { get; set; }
        public Guid UserId { get; set; }
        public string Status { get; set; }
        public decimal ProgressPercent { get; set; }
        public DateTime SubscribedAt { get; set; }
        public DateTime? SubscriptionEndsAt { get; set; }
    }
}
