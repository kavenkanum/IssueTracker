using IssueTracker.Domain.Entities;
using System;

namespace IssueTracker.Domain.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IssueTrackerDbContext _issueTrackerDbContext;
        public TaskRepository(IssueTrackerDbContext issueTrackerDbContext)
        {
            _issueTrackerDbContext = issueTrackerDbContext;
        }
        public void Add(string name, DateTime deadline, string description, int userId, Priority priority, int startsAfterTaskId, int projectId)
        {
            var newTask = new Task();
            newTask.Name = name;
            newTask.Deadline = deadline;
            newTask.Description = description;
            newTask.AssignedUserId = userId;
            newTask.Priority = priority;
            newTask.StartsAfterTaskId = startsAfterTaskId;
            newTask.DateOfCreate = DateTime.Now;
            newTask.ProjectId = projectId;
            newTask.Status = Status.New;
            _issueTrackerDbContext.Tasks.Add(newTask);
            _issueTrackerDbContext.SaveChanges();

        }

        public void Commit()
        {
            _issueTrackerDbContext.SaveChanges();
        }

        public bool Delete(int taskId)
        {
            var task = _issueTrackerDbContext.Tasks.Find(taskId);
            if (task != null)
            {
                _issueTrackerDbContext.Tasks.Remove(task);
                _issueTrackerDbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
