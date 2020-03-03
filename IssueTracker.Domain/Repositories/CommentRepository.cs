using IssueTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Domain.Repositories
{
    public interface ICommentRepository
    {
        void Add(int userId, int taskId, string description);
        bool Delete(int commentId);
        void Commit();
    }
    public class CommentRepository : ICommentRepository
    {
        private readonly IssueTrackerDbContext _issueTrackerDbContext;
        public CommentRepository(IssueTrackerDbContext issueTrackerDbContext)
        {
            _issueTrackerDbContext = issueTrackerDbContext;
        }
        public void Add(int userId, int taskId, string description)
        {
            var newComment = new Comment();
            newComment.UserId = userId;
            newComment.TaskId = taskId;
            newComment.Description = description;
            newComment.DateOfComment = DateTime.Now;
            _issueTrackerDbContext.Comments.Add(newComment);
            _issueTrackerDbContext.SaveChanges();
        }

        public void Commit()
        {
            _issueTrackerDbContext.SaveChanges();
        }

        public bool Delete(int commentId)
        {
            var comment = _issueTrackerDbContext.Comments.Find(commentId);
            if (comment != null)
            {
                _issueTrackerDbContext.Comments.Remove(comment);
                _issueTrackerDbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
