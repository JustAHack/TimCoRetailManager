using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDataManager.Library.Internal.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMDataManager.Library.DataAccess
{
	public class UserData
	{
		public List<UserModel> GetUserById(string id)
		{
			SQLDataAccess sql = new SQLDataAccess();
			var p = new { Id = id };

			List<UserModel> output = sql.LoadData<UserModel, dynamic>("dbo.spUserLookup", p, "TRMData");

			return output;
		}
	}
}
