using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Domain.Language
{
    public enum Trigger
    {
        StartJob,
        ChangeDeadline,
        EditProperties,
        ChangeName,
        ChangeDescription,
        ChangeAssignedUser,
        ChangePriority,
        FinishJob
    }
}
