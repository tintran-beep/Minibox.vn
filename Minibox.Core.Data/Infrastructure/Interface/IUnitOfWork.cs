using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using Minibox.Core.Data.Database;

namespace Minibox.Core.Data.Infrastructure.Interface
{
	public interface IUnitOfWork<TContext> : IDisposable, IAsyncDisposable
	{
		/// <summary>
		/// Saves all changes made in the current unit of work asynchronously.
		/// If the operation is inside an existing transaction, it will not commit the transaction.
		/// </summary>
		/// <param name="cancellationToken">A token to cancel the operation.</param>
		/// <param name="isPartOfTransaction">Indicates whether this save operation is part of an already opened transaction. 
		/// If true, changes will be saved but not committed.</param>
		/// <returns>The number of state entries written to the database.</returns>
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default, bool isPartOfTransaction = false);

		/// <summary>
		/// Saves multiple changes to the database in a bulk operation asynchronously.
		/// Useful for performance optimization when working with large datasets.
		/// </summary>
		/// <param name="cancellationToken">A token to cancel the operation.</param>
		/// <returns>The number of state entries written to the database.</returns>
		Task<int> BulkSaveChangesAsync(CancellationToken cancellationToken = default);

		/// <summary>
		/// Provides access to a generic repository for a specific entity type.
		/// </summary>
		/// <typeparam name="TEntity">The entity type to retrieve the repository for.</typeparam>
		/// <returns>An instance of <see cref="IRepository{TContext, TEntity}"/> for managing entity operations.</returns>
		IRepository<TContext, TEntity> Repository<TEntity>() where TEntity : BaseEntity;

		/// <summary>
		/// Begins a new database transaction asynchronously.
		/// </summary>
		/// <param name="cancellationToken">A token to cancel the operation.</param>
		/// <returns>An instance of <see cref="IDbContextTransaction"/> representing the transaction.</returns>
		Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

		/// <summary>
		/// Commits the current database transaction.
		/// </summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		Task CommitTransactionAsync();

		/// <summary>
		/// Rolls back the current database transaction, undoing all changes made within the transaction.
		/// </summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		Task RollbackTransactionAsync();

		/// <summary>
		/// Executes a stored procedure asynchronously and retrieves the results.
		/// </summary>
		/// <typeparam name="TResult">The type of the result expected from the stored procedure.</typeparam>
		/// <param name="storedProcedureName">The name of the stored procedure to execute.</param>
		/// <param name="parameters">The parameters required by the stored procedure.</param>
		/// <returns>A list of results of type <typeparamref name="TResult"/>.</returns>
		Task<List<TResult>> ExecStoredProcedureAsync<TResult>(string storedProcedureName, params SqlParameter[] parameters);
	}
}
