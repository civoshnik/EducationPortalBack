using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Result.Domain.Models
{
    public class ResultEntity
    {
        [Key]
        public Guid ResultId { get; set; }
        public Guid UserId { get; set; }
        public Guid TestId { get; set; }
        public int AttemptNumber { get; set; }
        public int Rating { get; set; }
        public int MaxRating { get; set; }
        public bool IsPassed { get; set; }
        public DateTime CompletedAt { get; set; }
        public DateTime? WastedAt { get; set; }
    }
}
