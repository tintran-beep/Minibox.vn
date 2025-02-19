using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Minibox.Core.Data.Database;
using Minibox.Core.Data.Extension;
using Minibox.Core.Data.Infrastructure.Interface;
using Minibox.Shared.Library.Extension;
using Minibox.Shared.Library.Setting;
using System.Reflection;

namespace Minibox.Core.Data.Infrastructure.Implementation
{
	public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : BaseDbContext
	{
		private readonly TContext _dbContext;
		private readonly MiniboxSettings _appSettings;
		private readonly Dictionary<Type, object> _repositories;

		public UnitOfWork(TContext dbContext, IOptions<MiniboxSettings> appSettings)
		{
			_dbContext = dbContext;
			_appSettings = appSettings.Value;
			_repositories = [];
		}

		/// <summary>
		/// Return Repository of type TEntity
		/// </summary>
		/// <typeparam name="TEntity"></typeparam>
		/// <returns></returns>
		public IRepository<TContext, TEntity> Repository<TEntity>() where TEntity : BaseEntity
		{
			var entityType = typeof(TEntity);

			if (_repositories.ContainsKey(entityType) == false)
			{
				var repositoryInstance = new Repository<TContext, TEntity>(_dbContext);
				_repositories.Add(entityType, repositoryInstance);
			}
			return (Repository<TContext, TEntity>)_repositories[entityType];
		}

		/// <summary>
		/// EF Savechanges
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			return await MiniboxExtensions.RetryHelper.RetryAsync(async () =>
			{
				var isSaveChange = _dbContext.ChangeTracker.Entries().Any(x => x.State == EntityState.Added
																			|| x.State == EntityState.Deleted
																			|| x.State == EntityState.Modified);
				if (isSaveChange)
				{
					using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
					try
					{
						var result = await _dbContext.SaveChangesAsync(cancellationToken);
						await transaction.CommitAsync(cancellationToken);

						_dbContext.DetachAllEntities();

						return result;
					}
					catch (Exception)
					{
						await transaction.RollbackAsync(cancellationToken);
						throw;
					}
				}
				return 0;
			}, TimeSpan.FromSeconds(_appSettings.RetrySettings.RetryIntervalInSeconds), _appSettings.RetrySettings.RetryMaxAttemptCount);
		}

		/// <summary>
		/// Bulk action Savechanges
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public virtual async Task<int> BulkSaveChangesAsync(CancellationToken cancellationToken = default)
		{
			return await MiniboxExtensions.RetryHelper.RetryAsync(async () =>
			{
				var rowEffects = 0;
				var isSaveChange = _dbContext.ChangeTracker.Entries().Any(x => x.State == EntityState.Added
																			|| x.State == EntityState.Deleted
																			|| x.State == EntityState.Modified);
				if (isSaveChange)
				{
					if (_dbContext.Database.GetDbConnection() is SqlConnection connection)
					{
						if (connection.State != System.Data.ConnectionState.Open)
							await connection.OpenAsync();

						using var transaction = await connection.BeginTransactionAsync(cancellationToken) as SqlTransaction;
						if (transaction != null)
						{
							try
							{
								var entities = _dbContext.GetEntities();
								if (entities.Any())
								{
									foreach (var item in entities)
									{
										var insertedEntities = item.Value.insertedEntities;
										if (insertedEntities != null && insertedEntities.Any())
										{
											rowEffects += await _dbContext.BulkInsertEntitiesAsync(insertedEntities, connection, transaction, _appSettings.DbContextSettings.CmdTimeOutInMiliseconds, _appSettings.DbContextSettings.BatchSize);
										}

										var updatedEntities = item.Value.updatedEntities;
										if (updatedEntities != null && updatedEntities.Any())
										{
											rowEffects += await _dbContext.BulkUpdateEntitiesAsync(updatedEntities, connection, transaction, _appSettings.DbContextSettings.CmdTimeOutInMiliseconds, _appSettings.DbContextSettings.BatchSize);
										}

										var deletedEntities = item.Value.deletedEntities;
										if (deletedEntities != null && deletedEntities.Any())
										{
											rowEffects += await _dbContext.BulkDeleteEntitiesAsync(deletedEntities, connection, transaction, _appSettings.DbContextSettings.CmdTimeOutInMiliseconds, _appSettings.DbContextSettings.BatchSize);
										}
									}
								}

								await transaction.CommitAsync(cancellationToken);

								_dbContext.DetachAllEntities();
							}
							catch (Exception)
							{
								await transaction.RollbackAsync(cancellationToken);
								throw;
							}

						}
					}
				}
				return rowEffects;
			}, TimeSpan.FromSeconds(_appSettings.RetrySettings.RetryIntervalInSeconds), _appSettings.RetrySettings.RetryMaxAttemptCount);
		}

		/// <summary>
		/// Exec a StoredProcedure
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="storedProcedureName"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public virtual async Task<List<TResult>> ExecStoredProcedureAsync<TResult>(string storedProcedureName, params SqlParameter[] parameters)
		{
			using var command = _dbContext.Database.GetDbConnection().CreateCommand();
			command.CommandText = storedProcedureName;
			command.CommandType = System.Data.CommandType.StoredProcedure;
			if (parameters.Any())
			{
				command.Parameters.Clear();
				command.Parameters.AddRange(parameters);
			}
			await _dbContext.Database.OpenConnectionAsync();

			var result = new List<TResult>();
			using var commandResult = await command.ExecuteReaderAsync();

			while (await commandResult.ReadAsync())
			{
				var obj = Activator.CreateInstance<TResult>();
				var propertyInfos = obj?.GetType()?.GetProperties();
				if (propertyInfos != null)
				{
					foreach (PropertyInfo prop in propertyInfos)
					{
						if (!Equals(commandResult[prop.Name], DBNull.Value))
						{
							prop.SetValue(obj, commandResult[prop.Name], null);
						}
					}
					result.Add(obj);
				}
			}
			await _dbContext.Database.CloseConnectionAsync();
			return result;
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
