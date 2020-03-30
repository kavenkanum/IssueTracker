using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Domain.Language.ValueObjects
{
    public class UserId
    {
        private UserId(Guid id)
        {
            Id = id;
        }
        public Guid Id { get;}

        public static Result<UserId> Create()
        {
            return Result.Ok(new UserId(Guid.NewGuid()));
        }
    }
}
