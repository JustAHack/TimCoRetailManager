using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using TRMDataManager.Library.DataAccess;
using TRMDataManager.Library.Internal.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMApi.Controllers
{
	[Authorize(Roles = "Cashier")]
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductData _productData;

		public ProductController(IProductData productData)
		{
			_productData = productData;
		}

		[HttpGet]
		public List<ProductModel> Get()
		{
			return _productData.GetProducts();
		}
	}
}
