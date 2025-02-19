using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;
using Minibox.Core.Data.Database;
using Minibox.Core.Data.Infrastructure.Interface;

namespace Minibox.Core.Data.Infrastructure.Implementation
{
	public class Repository<TContext, TEntity> : IRepository<TContext, TEntity>
		where TContext : BaseDbContext
		where TEntity : BaseEntity
	{
		private readonly TContext _dbContext;
		private readonly DbSet<TEntity> _dbSet;
		public Repository(TContext dbContext)
		{
			_dbContext = dbContext;
			_dbSet = _dbContext.Set<TEntity>();
		}

		public virtual void Insert(TEntity entity)
		{
			_dbSet.Add(entity);
		}

		public virtual void Insert(params TEntity[] entities)
		{
			_dbSet.AddRange(entities);
		}

		public virtual void Insert(IEnumerable<TEntity> entities)
		{
			_dbSet.AddRange(entities);
		}

		public virtual void Update(TEntity entity)
		{
			_dbSet.Update(entity);
		}

		public virtual void Update(params TEntity[] entities)
		{
			_dbSet.UpdateRange(entities);
		}

		public virtual void Update(IEnumerable<TEntity> entities)
		{
			_dbSet.UpdateRange(entities);
		}

		public virtual void Delete(TEntity entity)
		{
			_dbSet.Remove(entity);
		}

		public virtual void Delete(params TEntity[] entities)
		{
			_dbSet.RemoveRange(entities);
		}

		public virtual void Delete(IEnumerable<TEntity> entities)
		{
			_dbSet.RemoveRange(entities);
		}

		public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
		{
			_dbSet.RemoveRange(_dbSet.Where(predicate));
		}

		public virtual IQueryable<TEntity> Query(bool isNoTracking = false)
		{
			if (isNoTracking)
				return _dbSet.AsNoTracking().AsQueryable();
			return _dbSet.AsQueryable();
		}

		public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate, bool isNoTracking = false)
		{
			if (isNoTracking)
				return _dbSet.AsNoTracking().Where(predicate);
			return _dbSet.Where(predicate);
		}

		#region Implement Dispose Object
		public virtual void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		public virtual async ValueTask DisposeAsync()
		{
			await DisposeAsyncCore().ConfigureAwait(false);
			Dispose(disposing: false);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_dbContext is null)
					return;
				_dbContext.Dispose();

			}
		}

		protected virtual async ValueTask DisposeAsyncCore()
		{
			if (_dbContext is not null)
			{
				await _dbContext.DisposeAsync().ConfigureAwait(false);
			}
		}
		#endregion
	}
}
