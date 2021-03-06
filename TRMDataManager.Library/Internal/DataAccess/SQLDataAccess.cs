using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TRMDataManager.Library.Internal.DataAccess
{
	public class SQLDataAccess : IDisposable, ISQLDataAccess
	{

		private IDbConnection _connection;
		private IDbTransaction _transaction;
		private bool isClosed = false;
		private readonly ILogger _logger;
		private readonly IConfiguration _configuration;

		public SQLDataAccess(ILogger<ISQLDataAccess> logger, IConfiguration configuration)
		{
			_logger = logger;
			_configuration = configuration;
		}

		public string GetConnectionString(string name)
		{
			var conn = _configuration.GetConnectionString(name);
			return conn;
		}

		public List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName)
		{
			string connectionString = GetConnectionString(connectionStringName);
			using (IDbConnection cnn = new SqlConnection(connectionString))
			{
				List<T> rows = cnn.Query<T>(storedProcedure, parameters,
					commandType: CommandType.StoredProcedure).ToList();

				return rows;
			}
		}

		public void SaveData<T>(string storedProcedure, T parameters, string connectionStringName)
		{
			string connectionString = GetConnectionString(connectionStringName);
			using (IDbConnection cnn = new SqlConnection(connectionString))
			{
				_ = cnn.Execute(storedProcedure, parameters,
					commandType: CommandType.StoredProcedure);
			}
		}

		public void StartTransaction(string connectionStringName)
		{
			string connectionString = GetConnectionString(connectionStringName);
			_connection = new SqlConnection(connectionString);
			_connection.Open();
			_transaction = _connection.BeginTransaction();
			isClosed = false;
		}

		public void SaveDataInTransaction<T>(string storedProcedure, T parameters)
		{
			_connection.Execute(storedProcedure, parameters,
					commandType: CommandType.StoredProcedure, transaction: _transaction);
		}

		public List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters)
		{
			List<T> rows = _connection.Query<T>(storedProcedure, parameters,
					commandType: CommandType.StoredProcedure, transaction: _transaction).ToList();

			return rows;
		}

		public void CommitTransaction()
		{
			_transaction?.Commit();
			_connection?.Close();
			isClosed = true;
		}

		public void RollbackTransaction()
		{
			_transaction?.Rollback();
			_connection?.Close();
			isClosed = true;
		}

		public void Dispose()
		{
			if (isClosed == false)
			{
				try
				{
					CommitTransaction();
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Commit transaction failed in the dispose method.");
				}
			}

			_transaction = null;
			_connection = null;
		}
	}
}
