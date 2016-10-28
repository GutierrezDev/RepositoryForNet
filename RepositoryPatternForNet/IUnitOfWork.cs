using System;
using System.Threading.Tasks;

namespace RepositoryPatternForNet
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Returns an instance of the repository class with the given entity.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>   
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        /// <summary>
        /// Saves all changes in context to the underlying database.
        /// </summary>
        int SaveChanges();

        /// <summary>
        /// Asynchronously saves all changes in context to the underlying database.
        /// </summary>
        Task<int> SaveChangesAsync();
    }
}
