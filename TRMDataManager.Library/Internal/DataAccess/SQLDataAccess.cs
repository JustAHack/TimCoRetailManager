using System;
using System.Collections.Generic;
using System.Configuration;
using Dapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace TRMDataManager.Library.Internal.DataAccess
{
	internal class SQLDataAccess
	{
		public string GetConnectionString(string name)
		{
			return ConfigurationManager.ConnectionStrings[name].ConnectionString;
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
	}
}
