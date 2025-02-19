using Microsoft.Data.SqlClient;
using Minibox.Core.Data.Database;

namespace Minibox.Core.Data.Infrastructure.Interface
{
	public interface IUnitOfWork<TContext> : IDisposable, IAsyncDisposable
	{
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
		Task<int> BulkSaveChangesAsync(CancellationToken cancellationToken = default);
		IRepository<TContext, TEntity> Repository<TEntity>() where TEntity : BaseEntity;
		Task<List<TResult>> ExecStoredProcedureAsync<TResult>(string storedProcedureName, params SqlParameter[] parameters);
	}
}
