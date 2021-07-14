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
		public List<InventoryModel> GetInventory() {
			SQLDataAccess sql = new SQLDataAccess();

			var output = sql.LoadData<InventoryModel, dynamic>("dbo.spInventory_GetAll", new { }, "TRMData");

			return output;
		}

		public void SaveInventoryRecord(InventoryModel item)
		{
			SQLDataAccess sql = new SQLDataAccess();

			sql.SaveData("dbo.spInventory_Insert", item, "TRMData");
		}
	}
}
