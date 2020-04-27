using CSharpFunctionalExtensions;
using IssueTracker.Domain.Language.ValueObjects;
using System;

namespace IssueTracker.Domain.Entities
{
    public class Comment
    {
        //JobId, Userid
        private Comment(string description, int jobId, long userId, DateTime dateOfComment)
        {
            Description = description;
            JobId = jobId;
            UserId = userId;
            DateOfComment = dateOfComment;
        }
        public int Id { get; private set; }
        public int JobId { get; private set; }
        public string Description { get; private set; }
        public long UserId { get; private set; }
        public DateTime DateOfComment { get; private set; }

        public static Result<Comment> Create(string description, int jobId, long userId, DateTime dateOfComment)
        {
            return Result.Create(!string.IsNullOrEmpty(description), "Comment cannot be empty")
                .OnSuccess(() => new Comment(description, jobId, userId, dateOfComment));
        }

    }
}