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

		public InventoryController(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		[Authorize(Roles = "Manager,Admin")]
		public List<InventoryModel> Get()
		{
			InventoryData data = new InventoryData(_configuration);
			return data.GetInventory();
		}

		[Authorize(Roles = "Admin")]
		public void Post(InventoryModel item)
		{
			InventoryData data = new InventoryData(_configuration);
			data.SaveInventoryRecord(item);
		}
	}
}