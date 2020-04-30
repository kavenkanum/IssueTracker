using CSharpFunctionalExtensions;
using System.Threading.Tasks;

namespace IssueTracker.Domain.Repositories
{
    public interface IProjectRepository
    {
        Task<Maybe<Project>> GetAsync(int projectId);
        Task<Result<int>> SaveAsync(Project project);
        Result Delete(Project project);
    }
}
