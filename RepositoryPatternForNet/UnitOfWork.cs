using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace RepositoryPatternForNet
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext context;

        /// <summary>
        /// Constructor for the class.
        /// </summary>
        /// <param name="dbContext">An instance of the entity framework context.</param>
        public UnitOfWork(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException();

            context = dbContext;
        }

        /// <summary>
        /// Returns an instance of the repository class with the given entity.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>        
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new Repository<TEntity>(context);
        }

        /// <summary>
        /// Saves all changes in context to the underlying database.
        /// </summary>
        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        /// <summary>
        /// Asynchronously saves all changes in context to the underlying database.
        /// </summary>
        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
                if (disposing)
                    context.Dispose();

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
