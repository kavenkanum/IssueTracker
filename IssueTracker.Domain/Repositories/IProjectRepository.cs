namespace IssueTracker.Domain.Repositories
{
    public interface IProjectRepository
    {
        void Add(string name);
        bool Delete(int projectId);
        void Commit();
    }
}
