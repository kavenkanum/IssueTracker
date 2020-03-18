using CSharpFunctionalExtensions;
using IssueTracker.Domain;
using IssueTracker.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Maybe<Project>> GetAsync(int projectId)
        {
            return await _issueTrackerDbContext.Projects
                .Include(p => p.Jobs)
                .SingleOrDefaultAsync(p => p.Id == projectId);
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
