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
	public class ProductData
	{
		private readonly IConfiguration _configuration;

		public ProductData(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public List<ProductModel> GetProducts()
		{
			SQLDataAccess sql = new SQLDataAccess(_configuration);

			var output = sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetAll", new { }, "TRMData");

			return output;
		}

		public ProductModel GetProductById(int productId)
		{
			SQLDataAccess sql = new SQLDataAccess(_configuration);

			var output = sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetById", new { Id = productId }, "TRMData").FirstOrDefault();

			return output;
		}
	}
}
