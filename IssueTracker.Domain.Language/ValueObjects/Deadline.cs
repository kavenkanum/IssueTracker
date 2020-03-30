using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Domain.Language.ValueObjects
{
    public class Deadline
    {
        private Deadline(DateTime deadlineDate)
        {
            DeadlineDate = deadlineDate;
        }
        public DateTime DeadlineDate { get; }

        public static Result<Deadline> Create(DateTime deadlineDate, DateTime currentDate)
        {
            return Result.Create(deadlineDate > currentDate.AddDays(1), "Deadline cannot be earlier than now")
                .OnSuccess(() => new Deadline(deadlineDate));
        }
    }
}
