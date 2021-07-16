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
	public class InventoryData
	{
		private readonly IConfiguration _configuration;

		public InventoryData(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public List<InventoryModel> GetInventory() {
			SQLDataAccess sql = new SQLDataAccess(_configuration);

			var output = sql.LoadData<InventoryModel, dynamic>("dbo.spInventory_GetAll", new { }, "TRMData");

			return output;
		}

		public void SaveInventoryRecord(InventoryModel item)
		{
			SQLDataAccess sql = new SQLDataAccess(_configuration);

			sql.SaveData("dbo.spInventory_Insert", item, "TRMData");
		}
	}
}
