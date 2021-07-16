using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRMDataManager.Library.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMApi.Controllers
{
	[Authorize(Roles = "Cashier")]
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IConfiguration _configuration;

		public ProductController(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public List<ProductModel> Get()
		{
			ProductData data = new ProductData(_configuration);

			return data.GetProducts();
		}
	}
}
