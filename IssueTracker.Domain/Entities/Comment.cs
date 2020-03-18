using CSharpFunctionalExtensions;
using System;

namespace IssueTracker.Domain.Entities
{
    public class Comment
    {
        private Comment(string description, int jobId, string userId, DateTime dateOfComment)
        {
            Description = description;
            JobId = jobId;
            UserId = userId;
            DateOfComment = dateOfComment;
        }
        public int Id { get; private set; }
        public int JobId { get; private set; }
        public string Description { get; private set; }
        public string UserId { get; private set; }
        public DateTime DateOfComment { get; private set; }

        public static Result<Comment> Create(string description, int jobId, string userId, DateTime dateOfComment)
        {
            return Result.Create(!string.IsNullOrEmpty(description), "Comment cannot be empty")
                .OnSuccess(() => new Comment(description, jobId, userId, dateOfComment));
        }

    }
}