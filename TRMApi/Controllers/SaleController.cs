using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using TRMDataManager.Library.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMApi.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class SaleController : ControllerBase
	{
		private readonly ISaleData _saleData;

		public SaleController(ISaleData saleData)
		{
			_saleData = saleData;
		}

		[Authorize(Roles = "Cashier")]
		[HttpPost]
		public void Post(SaleModel sale)
		{
			var cashierId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			_saleData.SaveSale(sale, cashierId);
		}

		[Authorize(Roles = "Manager,Admin")]
		[Route("GetSalesReport")]
		[HttpGet]
		public List<SaleReportModel> GetSalesReport()
		{
			return _saleData.GetSaleReport();
		}
	}
}
