using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using TRMDataManager.Library.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class InventoryController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		private readonly IInventoryData _inventoryData;

		public InventoryController(IConfiguration configuration, IInventoryData inventoryData)
		{
			_configuration = configuration;
			_inventoryData = inventoryData;
		}

		[Authorize(Roles = "Manager,Admin")]
		[HttpGet]
		public List<InventoryModel> Get()
		{
			return _inventoryData.GetInventory();
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public void Post(InventoryModel item)
		{
			_inventoryData.SaveInventoryRecord(item);
		}
	}
}