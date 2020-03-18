using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Commands
{
    public interface IDataProvider
    {
        DateTime GetCurrentDate();
    }
    public class DataProvider : IDataProvider
    {
        public DateTime GetCurrentDate()
        {
            return DateTime.Now;
        }
    }
}
