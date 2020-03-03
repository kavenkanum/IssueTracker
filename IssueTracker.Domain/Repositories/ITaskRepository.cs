using IssueTracker.Domain.Entities;
using System;

namespace IssueTracker.Domain.Repositories
{
    public interface ITaskRepository
    {
        void Add(string name, DateTime deadline, string description, int userId, Priority priority, int startsAfterTaskId, int projectId);
        bool Delete(int taskId);
        void Commit();
    }
}
