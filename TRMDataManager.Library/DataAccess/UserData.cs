using Microsoft.Extensions.Configuration;
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
		private readonly IConfiguration _configuration;

		public UserData(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public List<UserModel> GetUserById(string id)
		{
			SQLDataAccess sql = new SQLDataAccess(_configuration);
			var p = new { Id = id };

			List<UserModel> output = sql.LoadData<UserModel, dynamic>("dbo.spUserLookup", p, "TRMData");

			return output;
		}
	}
}
