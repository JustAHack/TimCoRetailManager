﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDataManager.Library.Internal.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMDataManager.Library.DataAccess
{
	public class SaleData
	{
		public void SaveSale(SaleModel saleInfo, string cashierId)
		{
			// TODO: Make this SOLID / DRY / BETTER
			List<SaleDetailDBModel> details = new List<SaleDetailDBModel>();
			ProductData products = new ProductData();
			var taxRate = ConfigHelper.GetTaxRate() / 100;

			foreach (var item in saleInfo.SaleDetails)
			{
				var detail = new SaleDetailDBModel
				{
					ProductId = item.ProductId,
					Quantity = item.Quantity
				};

				var productInfo = products.GetProductById(detail.ProductId);

				if (productInfo == null)
				{
					throw new Exception($"The product id of {detail.ProductId} could not be found in the database.");
				}

				detail.PurchasePrice = productInfo.RetailPrice * detail.Quantity;

				if (productInfo.IsTaxable)
				{
					detail.Tax = detail.PurchasePrice * taxRate;
				}

				details.Add(detail);
			}

			SaleDBModel sale = new SaleDBModel
			{
				SubTotal = details.Sum(x => x.PurchasePrice),
				Tax = details.Sum(x => x.Tax),
				CashierId = cashierId
			};

			sale.Total = sale.SubTotal + sale.Tax;

			SQLDataAccess sql = new SQLDataAccess();
			sql.SaveData("dbo.spSale_Insert", sale, "TRMData");

			sale.Id = sql.LoadData<int, dynamic>("spSale_Lookup", new { sale.CashierId, sale.SaleDate }, "TRMData").FirstOrDefault();

			foreach (var item in details)
			{
				item.SaleId = sale.Id;
				sql.SaveData("dbo.spSaleDetail_Insert", item, "TRMData");
			}
		}
	}
}
