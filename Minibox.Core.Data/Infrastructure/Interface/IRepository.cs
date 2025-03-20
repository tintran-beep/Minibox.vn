using System.Linq.Expressions;

namespace Minibox.Core.Data.Infrastructure.Interface
{
	public interface IRepository<TContext, TEntity> : IDisposable, IAsyncDisposable
	{
		void Insert(TEntity entity);
		void Insert(params TEntity[] entities);
		void Insert(IEnumerable<TEntity> entities);

		void Update(TEntity entity);
		void Update(params TEntity[] entities);
		void Update(IEnumerable<TEntity> entities);

		void Delete(TEntity entity);
		void Delete(params TEntity[] entities);
		void Delete(IEnumerable<TEntity> entities);
		void Delete(Expression<Func<TEntity, bool>> predicate);

		IQueryable<TEntity> Query(bool isNoTracking = false);
		IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate, bool isNoTracking = false);


		#region Asynchronously
		/// <summary>
		/// Insert a new entity asynchronously.
		/// </summary>
		Task InsertAsync(TEntity entity);

		/// <summary>
		/// Insert multiple entities asynchronously.
		/// </summary>
		Task InsertRangeAsync(IEnumerable<TEntity> entities);
		
		/// <summary>
		/// Delete an entity by its ID asynchronously.
		/// </summary>
		Task DeleteByIdAsync(Guid id);

		/// <summary>
		/// Get an entity by its ID asynchronously.
		/// </summary>
		Task<TEntity?> GetByIdAsync(Guid id);

		/// <summary>
		/// Get all entities asynchronously.
		/// </summary>
		Task<IEnumerable<TEntity>> GetAllAsync();

		/// <summary>
		/// Find entities based on a predicate asynchronously.
		/// </summary>
		Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

		/// <summary>
		/// Get the first entity matching the condition asynchronously.
		/// </summary>
		Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

		/// <summary>
		/// Check if any entity matches the given condition.
		/// </summary>
		Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);

		/// <summary>
		/// Count the number of entities that match the given condition asynchronously.
		/// </summary>
		Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
		#endregion
	}
}
