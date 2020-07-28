using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Domain.Entities
{
    public class LoggedTime
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public long UserId { get; set; }
        public double LoggedTimeSlice { get; set; }
    }
}
