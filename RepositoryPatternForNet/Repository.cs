using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RepositoryPatternForNet
{
    internal class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext context;
        private readonly DbSet<TEntity> dbSet;

        /// <summary>
        /// Constructor for the class.
        /// </summary>
        /// <param name="dbContext">An instance of the entity framework context.</param>
        public Repository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException();

            context = dbContext;
            dbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// Returns queryable entity collection from context.
        /// </summary>
        /// <param name="filter">Condition.</param>
        /// <param name="orderBy">Sorting condition.</param>
        /// <param name="includeProperties">Objects to return in the query result.</param>
        /// <param name="skip">The number of elements to be skipped.</param>
        /// <param name="take">The number of elements to be taken.</param>
        private IQueryable<TEntity> GetQueryable(
               Expression<Func<TEntity, bool>> filter = null,
               Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
               string[] includeProperties = null,
               int? skip = null,
               int? take = null)
        {
            IQueryable<TEntity> query = dbSet;

            foreach (string includeProperty in includeProperties)
                if (!String.IsNullOrEmpty(includeProperty))
                    query = query.Include(includeProperty);

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return query;
        }

        /// <summary>
        /// Returns all entities from context.
        /// </summary>
        public List<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        /// <summary>
        /// Asynchronously returns all entities from context.
        /// </summary>
        public async Task<List<TEntity>> GetAllAsync()
        {            
            return await dbSet.ToListAsync();
        }

        /// <summary>
        /// Returns enumerable entity collection from context.
        /// </summary>
        /// <param name="filter">Condition.</param>
        /// <param name="orderBy">Sorting condition.</param>
        /// <param name="includeProperties">Objects to return in the query result.</param>
        /// <param name="skip">The number of elements to be skipped.</param>
        /// <param name="take">The number of elements to be taken.</param>
        public List<TEntity> Get(
               Expression<Func<TEntity, bool>> filter = null,
               Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
               string[] includeProperties = null,
               int? skip = null,
               int? take = null)
        {
            return GetQueryable(filter, orderBy, includeProperties, skip, take).ToList();
        }

        /// <summary>
        /// Asynchronously returns enumerable entity collection from context.
        /// </summary>
        /// <param name="filter">Condition.</param>
        /// <param name="orderBy">Sorting condition.</param>
        /// <param name="includeProperties">Objects to return in the query result.</param>
        /// <param name="skip">The number of elements to be skipped.</param>
        /// <param name="take">The number of elements to be taken.</param>
        public async Task<List<TEntity>> GetAsync(
               Expression<Func<TEntity, bool>> filter = null,
               Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
               string[] includeProperties = null,
               int? skip = null,
               int? take = null)
        {
            return await GetQueryable(filter, orderBy, includeProperties, skip, take).ToListAsync();
        }

        /// <summary>
        /// Finds an entity with the given primary key value.
        /// </summary>
        /// <param name="id">The value of the primary key for the entity to be found.</param>
        public TEntity FindById(object id)
        {
            return dbSet.Find(id);
        }

        /// <summary>
        /// Asynchronously finds an entity with the given primary key value.
        /// </summary>
        /// <param name="id">The value of the primary key for the entity to be found.</param>
        public async Task<TEntity> FindByIdAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }

        /// <summary>
        /// Returns the first element of a sequence that satisfies a specified condition or a default value if no such element is found.
        /// </summary>
        /// <param name="filter">Condition.</param>
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter = null)
        {
            return dbSet.FirstOrDefault(filter);
        }

        /// <summary>
        /// Asynchronously returns the first element of a sequence that satisfies a specified condition or a default value if no such element is found.
        /// </summary>
        /// <param name="filter">Condition.</param>
        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return await dbSet.FirstOrDefaultAsync(filter);
        }

        /// <summary>
        /// Returns the number of elements in the specified sequence that satisfies a condition.
        /// </summary>
        /// <param name="filter">Condition.</param>        
        public int GetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter != null)
                return dbSet.Count(filter);
            else
                return dbSet.Count();
        }

        /// <summary>
        /// Asynchronously returns the number of elements in the specified sequence that satisfies a condition.
        /// </summary>
        /// <param name="filter">Condition.</param>   
        public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter != null)
                return await dbSet.CountAsync(filter);
            else
                return await dbSet.CountAsync();
        }

        /// <summary>
        /// Determines whether any element of a sequence satisfies a condition.
        /// </summary>
        /// <param name="filter">Condition.</param>
        public bool IsExist(Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter != null)
                return dbSet.Any(filter);
            else
                return dbSet.Any();
        }

        /// <summary>
        /// Asynchronously determines whether any element of a sequence satisfies a condition.
        /// </summary>
        /// <param name="filter">Condition.</param>
        public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter != null)
                return await dbSet.AnyAsync(filter);
            else
                return await dbSet.AnyAsync();
        }

        /// <summary>
        /// Removes an entity with the given primary key value from context.
        /// </summary>
        /// <param name="id">The value of the primary key for the entity to be deleted.</param>
        public void Delete(object id)
        {
            TEntity entity = FindById(id);
            Delete(entity);
        }

        /// <summary>
        /// Removes the given entity from context.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        public void Delete(TEntity entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
                dbSet.Attach(entity);

            dbSet.Remove(entity);
        }

        /// <summary>
        /// Removes the given collection from context.
        /// </summary>
        /// <param name="entityList">The collection of entities to be deleted.</param>
        public void DeleteRange(List<TEntity> entityList)
        {
            dbSet.RemoveRange(entityList);
        }

        /// <summary>
        /// Adds entity into context.
        /// </summary>
        /// <param name="entity">The entity to be added.</param>
        public void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        /// <summary>
        /// Adds the given collection of entities into context.
        /// </summary>
        /// <param name="entityList">The collection of entities to be added.</param>
        public void InsertRange(List<TEntity> entityList)
        {
            dbSet.AddRange(entityList);
        }

        /// <summary>
        /// Updates entity.
        /// </summary>
        /// <param name="entity">The entity to be updated.</param>
        public void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Updates the given collection.
        /// </summary>
        /// <param name="entityList">The collection of entities to be updated.</param>
        public void UpdateRange(List<TEntity> entityList)
        {
            foreach (TEntity entity in entityList)
                Update(entity);
        }
    }
}
