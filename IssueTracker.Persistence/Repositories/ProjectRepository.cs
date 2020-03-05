using CSharpFunctionalExtensions;
using IssueTracker.Domain;
using IssueTracker.Domain.Repositories;
using System.Threading.Tasks;

namespace IssueTracker.Persistence.Repositories
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
            Maybe<Project> project = _issueTrackerDbContext.Projects.Find(projectId);
            //var project = _issueTrackerDbContext.Projects.Find(projectId);
            //Maybe<Project> project2 = project != null ? Maybe<Project>.From(project) : Maybe<Project>.None;
            return Task.FromResult(project);
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
