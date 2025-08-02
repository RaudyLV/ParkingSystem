
using Application.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;
using Infraestructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence.Repositories
{
    public class RepositoryAsync<T> : RepositoryBase<T>, IRepositoryAsync<T>
        where T : class
    {
        private readonly AppDbContext _dbContext;
        public RepositoryAsync(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task BeginTransactionAsync()
        {
            await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _dbContext.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _dbContext.Database.RollbackTransactionAsync();
        }

        public async Task ExecuteRawSqlAsync(string sql, params object[] parameters)
        {
            await _dbContext.Database.ExecuteSqlRawAsync(sql, parameters);
        }
    }
}