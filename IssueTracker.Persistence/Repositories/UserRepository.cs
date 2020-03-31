using CSharpFunctionalExtensions;
using IssueTracker.Domain.Entities;
using IssueTracker.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly IssueTrackerDbContext _issueTrackerDbContext;
        public UserRepository(IssueTrackerDbContext issueTrackerDbContext)
        {
            _issueTrackerDbContext = issueTrackerDbContext;
        }
        public Result Delete(User user)
        {
            //instead of deleting make nonactive
            _issueTrackerDbContext.Remove(user);
            return Result.Ok();
        }
        public async Task<Maybe<User>> GetAsync(Guid userId)
        {
            Maybe<User> user = await _issueTrackerDbContext.Users.FindAsync(userId);
            return user;
        }

    }
}
