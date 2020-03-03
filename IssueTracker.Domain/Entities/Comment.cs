using System;

namespace IssueTracker.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public DateTime DateOfComment { get; set; }
    }
}