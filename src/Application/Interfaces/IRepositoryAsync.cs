

using Ardalis.Specification;

namespace Application.Interfaces
{
    public interface IRepositoryAsync<T> : IRepositoryBase<T>
        where T : class
    {
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task ExecuteRawSqlAsync(string sql, params object[] parameters);
    }
}