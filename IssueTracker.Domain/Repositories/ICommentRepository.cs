namespace IssueTracker.Domain.Repositories
{
    public interface ICommentRepository
    {
        void Add(int userId, int taskId, string description);
        bool Delete(int commentId);
        void Commit();
    }
}
