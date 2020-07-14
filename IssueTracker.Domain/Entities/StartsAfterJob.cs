using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Domain.Entities
{
    public class StartsAfterJob
    {
        private StartsAfterJob(int startsAfterJobId)
        {
            StartsAfterJobId = startsAfterJobId;
        }
        public int Id { get; set; }
        public int JobId { get; set; }
        public int StartsAfterJobId { get; private set; }

        public static Result<StartsAfterJob> Create(int startsAfterJobId)
        {
            return Result.Create(startsAfterJobId != default, "Previous job id cannot be default.")
                .OnSuccess(() => new StartsAfterJob(startsAfterJobId));
        }
    }
}
