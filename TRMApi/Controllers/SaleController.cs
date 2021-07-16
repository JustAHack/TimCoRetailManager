using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
		private readonly IConfiguration _configuration;

		public SaleController(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		[Authorize(Roles = "Cashier")]
		public void Post(SaleModel sale)
		{
			var cashierId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			SaleData data = new SaleData(_configuration);
			data.SaveSale(sale, cashierId);
		}

		[Authorize(Roles = "Manager,Admin")]
		[Route("GetSalesReport")]
		public List<SaleReportModel> GetSalesReport()
		{
			SaleData data = new SaleData(_configuration);
			return data.GetSaleReport();
		}
	}
}
