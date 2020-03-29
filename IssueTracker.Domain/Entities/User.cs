using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace IssueTracker.Domain.Entities
{
    public class User 
    {
        private User(string userId, string fullName, string email)
        {
            UserId = userId;
            FullName = fullName;
            Email = email;
        }
        public Guid UserId { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public List<Job> AssignedTasks { get; set; }

        public static Result<User> Create(string userId, string fullName, string email)
        {
            return Result.Create(!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(userId), "User id, full name and email cannot be empty.")
                .OnSuccess(() => new User(userId, fullName, email));
        }

        public void AddJob(Job job)
        {
            AssignedTasks.Add(job);
        }
    }
}
