using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace IssueTracker.Domain.Entities
{
    public class User 
    {
        private User(long id, string fullName, string email)
        {
            Id = id;
            FullName = fullName;
            Email = email;
        }
        public long Id { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public List<Job> AssignedTasks { get; set; }
        public List<LoggedTime> LoggedTimes { get; set; }

        public static Result<User> Create(long userId, string fullName, string email)
        {
            return Result.Create(userId != default && !string.IsNullOrEmpty(fullName) && !string.IsNullOrEmpty(email), "User id, full name and email cannot be empty.")
                .OnSuccess(() => new User(userId, fullName, email));
        }

        public void AddJob(Job job)
        {
            AssignedTasks.Add(job);
        }
    }
}
