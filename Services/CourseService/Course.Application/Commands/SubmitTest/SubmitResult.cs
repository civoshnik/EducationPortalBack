using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Commands.SubmitTest
{
    public class SubmitResult
    {
        public bool Success { get; set; }
        public int Grade { get; set; }
        public int AssignedGrade { get; set; }
        public bool Passed { get; set; }
    }

}
