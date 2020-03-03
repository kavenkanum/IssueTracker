using CSharpFunctionalExtensions;
using System.Threading.Tasks;

namespace IssueTracker.Domain.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IssueTrackerDbContext _issueTrackerDbContext;
        public ProjectRepository(IssueTrackerDbContext issueTrackerDbContext)
        {
            _issueTrackerDbContext = issueTrackerDbContext;
        }

        public Result Delete(Project project)
        {
            _issueTrackerDbContext.Remove(project);
            return Result.Ok();
        }

        public Task<Maybe<Project>> GetAsync(int projectId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Result> SaveAsync(Project project)
        {
            if (project.Id == default)
            {
                await _issueTrackerDbContext.Projects.AddAsync(project);
            }

            await _issueTrackerDbContext.SaveChangesAsync();
            return Result.Ok();
        }
    }
}
