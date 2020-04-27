using CSharpFunctionalExtensions;
using IssueTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker.Domain.Repositories
{
    public interface IUserRepository
    {
        Result Delete(User user);
        Task<Maybe<User>> GetAsync(long userId);
    }
}
