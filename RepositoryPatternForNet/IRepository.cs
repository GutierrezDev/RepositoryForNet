using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RepositoryPatternForNet
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Returns all entities from context.
        /// </summary>
        List<TEntity> GetAll();

        /// <summary>
        /// Asynchronously returns all entities from context.
        /// </summary>
        Task<List<TEntity>> GetAllAsync();

        /// <summary>
        /// Returns enumerable entity collection from context.
        /// </summary>
        /// <param name="filter">Condition.</param>
        /// <param name="orderBy">Sorting condition.</param>
        /// <param name="includeProperties">Objects to return in the query result.</param>
        /// <param name="skip">The number of elements to be skipped.</param>
        /// <param name="take">The number of elements to be taken.</param>
        List<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string[] includeProperties = null,
            int? skip = null,
            int? take = null);

        /// <summary>
        /// Asynchronously returns enumerable entity collection from context.
        /// </summary>
        /// <param name="filter">Condition.</param>
        /// <param name="orderBy">Sorting condition.</param>
        /// <param name="includeProperties">Objects to return in the query result.</param>
        /// <param name="skip">The number of elements to be skipped.</param>
        /// <param name="take">The number of elements to be taken.</param>
        Task<List<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string[] includeProperties = null,
            int? skip = null,
            int? take = null);

        /// <summary>
        /// Finds an entity with the given primary key value.
        /// </summary>
        /// <param name="id">The value of the primary key for the entity to be found.</param>
        TEntity FindById(object id);

        /// <summary>
        /// Asynchronously finds an entity with the given primary key value.
        /// </summary>
        /// <param name="id">The value of the primary key for the entity to be found.</param>
        Task<TEntity> FindByIdAsync(object id);

        /// <summary>
        /// Returns the first element of a sequence that satisfies a specified condition or a default value if no such element is found.
        /// </summary>
        /// <param name="filter">Condition.</param>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Asynchronously returns the first element of a sequence that satisfies a specified condition or a default value if no such element is found.
        /// </summary>
        /// <param name="filter">Condition.</param>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Returns the number of elements in the specified sequence that satisfies a condition.
        /// </summary>
        /// <param name="filter">Condition.</param>
        int GetCount(Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// Asynchronously returns the number of elements in the specified sequence that satisfies a condition.
        /// </summary>
        /// <param name="filter">Condition.</param>   
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// Determines whether any element of a sequence satisfies a condition.
        /// </summary>
        /// <param name="filter">Condition.</param>
        bool IsExist(Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// Asynchronously determines whether any element of a sequence satisfies a condition.
        /// </summary>
        /// <param name="filter">Condition.</param>
        Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// Adds entity into context.
        /// </summary>
        /// <param name="entity">The entity to be added.</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Adds the given collection of entities into context.
        /// </summary>
        /// <param name="entityList">The collection of entities to be added.</param>
        void InsertRange(List<TEntity> entityList);

        /// <summary>
        /// Removes an entity with the given primary key value from context.
        /// </summary>
        /// <param name="id">The value of the primary key for the entity to be deleted.</param>
        void Delete(object id);

        /// <summary>
        /// Removes the given entity from context.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Removes the given collection from context.
        /// </summary>
        /// <param name="entityList">The collection of entities to be deleted.</param>
        void DeleteRange(List<TEntity> entityList);

        /// <summary>
        /// Updates entity.
        /// </summary>
        /// <param name="entity">The entity to be updated.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Updates the given collection.
        /// </summary>
        /// <param name="entityList">The collection of entities to be updated.</param>
        void UpdateRange(List<TEntity> entityList);
    }
}
