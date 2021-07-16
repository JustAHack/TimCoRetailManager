using System.Collections.Generic;
using TRMDataManager.Library.Internal.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMDataManager.Library.DataAccess
{
	public class UserData : IUserData
	{
		private readonly ISQLDataAccess _sql;

		public UserData(ISQLDataAccess sql)
		{
			_sql = sql;
		}

		public List<UserModel> GetUserById(string Id)
		{
			List<UserModel> output = _sql.LoadData<UserModel, dynamic>("dbo.spUserLookup", new { Id }, "TRMData");

			return output;
		}
	}
}
