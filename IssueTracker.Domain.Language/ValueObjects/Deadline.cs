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

        public static Result<Maybe<Deadline>> CreateOptional(DateTime? deadlineDate, DateTime currentDate)
        {
            if (!deadlineDate.HasValue)
                return Result.Ok(Maybe<Deadline>.None);

            return Result.Create(deadlineDate > currentDate.AddDays(1), "Deadline cannot be earlier than now")
                .OnSuccess(() => Maybe<Deadline>.From(new Deadline(deadlineDate.Value)));
        }
    }
}
