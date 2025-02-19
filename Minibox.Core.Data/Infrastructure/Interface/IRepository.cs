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
	}
}
