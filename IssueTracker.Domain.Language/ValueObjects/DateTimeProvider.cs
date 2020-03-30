using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Domain.Language.ValueObjects
{
    public interface IDateTimeProvider
    {
        DateTime GetCurrentDate();
    }
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetCurrentDate()
        {
            return DateTime.Now;
        }
    }
}
