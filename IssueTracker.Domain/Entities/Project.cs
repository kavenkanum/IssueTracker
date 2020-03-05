using CSharpFunctionalExtensions;
using IssueTracker.Domain.Entities;
using System.Collections.Generic;

namespace IssueTracker.Domain
{
    public class Project
    {
        private Project(string name)
        {
            Name = name;
            Tasks = new List<Task>();
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public List<Task> Tasks { get; private set; }

        public void ChangeName(string name)
        {
            Name = name;
        }

        public void AddTask(Task task)
        {
            Tasks.Add(task);
        }

        public static Result<Project> Create(string name)
        {
            return Result.Create(!string.IsNullOrWhiteSpace(name), "Project name cannot be empty")
                .OnSuccess(() => new Project(name));
        }
    }
}
