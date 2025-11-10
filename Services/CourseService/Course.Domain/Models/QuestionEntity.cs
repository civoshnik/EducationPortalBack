using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Domain.Models
{
    public class QuestionEntity
    {
        [Key]
        public Guid QuestionId { get; set; }
        public Guid TestId { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}

