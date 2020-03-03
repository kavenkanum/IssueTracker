using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IssueTracker.Domain.Repositories
{
    public interface IProjectRepository
    {
        void Add(string name);
        bool Delete(int projectId);
        void Commit();
    }
    public class ProjectRepository : IProjectRepository
    {
        private readonly IssueTrackerDbContext _issueTrackerDbContext;
        public ProjectRepository(IssueTrackerDbContext issueTrackerDbContext)
        {
            _issueTrackerDbContext = issueTrackerDbContext;
        }
        public void Add(string name)
        {
            var newProject = new Project();
            newProject.Name = name;
            _issueTrackerDbContext.Add(newProject);
            _issueTrackerDbContext.SaveChanges();
        }

        public void Commit()
        {
            _issueTrackerDbContext.SaveChanges();
        }

        public bool Delete(int projectId)
        {
            var project = _issueTrackerDbContext.Projects.Find(projectId);
            if (project != null)
            {
                _issueTrackerDbContext.Projects.Remove(project);
                _issueTrackerDbContext.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
