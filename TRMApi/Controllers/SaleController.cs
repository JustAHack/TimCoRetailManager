using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TRMDataManager.Library.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMApi.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class SaleController : ControllerBase
	{
		[Authorize(Roles = "Cashier")]
		public void Post(SaleModel sale)
		{
			var cashierId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			SaleData data = new SaleData();
			data.SaveSale(sale, cashierId);
		}

		[Authorize(Roles = "Manager,Admin")]
		[Route("GetSalesReport")]
		public List<SaleReportModel> GetSalesReport()
		{
			SaleData data = new SaleData();
			return data.GetSaleReport();
		}
	}
}
