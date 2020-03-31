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
            Jobs = new List<Job>();
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public List<Job> Jobs { get; private set; }

        public void ChangeName(string name)
        {
            Name = name;
        }

        public void AddJob(Job job)
        {
            Jobs.Add(job);
        }

        public static Result<Project> Create(string name)
        {
            return Result.Create(!string.IsNullOrWhiteSpace(name), "Project name cannot be empty or contain white spaces.")
                .OnSuccess(() => new Project(name));
        }
    }
}
