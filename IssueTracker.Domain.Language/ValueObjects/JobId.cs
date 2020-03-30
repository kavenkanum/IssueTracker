using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Domain.Language.ValueObjects
{
    public class JobId
    {
        private JobId(int id)
        {
            Id = id;
        }
        public int Id { get; }

        //public static Result<JobId> Create(int id)
        //{
        //    return Result.Create(id != default, "Id cannot be 0 value")
        //        .OnSuccess(() => new JobId(id));
        //}
    }
}
