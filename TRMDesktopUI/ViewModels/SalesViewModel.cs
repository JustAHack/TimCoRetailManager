using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDesktopUI.Library.Api;
using TRMDesktopUI.Library.Models;

namespace TRMDesktopUI.ViewModels
{
	public class SalesViewModel : Screen
	{
		private BindingList<ProductModel> _products;
		private BindingList<ProductModel> _cart;
		private int _itemQuantity;
		private IProductEndpoint _productEndpoint;

		public SalesViewModel(IProductEndpoint productEndpoint)
		{
			_productEndpoint = productEndpoint;
		}

		protected override async void OnViewLoaded(object view)
		{
			base.OnViewLoaded(view);
			await LoadProducts();
		}

		private async Task LoadProducts()
		{
			var productList = await _productEndpoint.GetAll();
			Products = new BindingList<ProductModel>(productList);
		}

		public BindingList<ProductModel> Products
		{
			get => _products;
			set
			{
				_products = value;
				NotifyOfPropertyChange(() => Products);
			}
		}

		public int ItemQuantity
		{
			get => _itemQuantity;
			set
			{
				_itemQuantity = value;
				NotifyOfPropertyChange(() => ItemQuantity);
			}
		}

		public BindingList<ProductModel> Cart
		{
			get => _cart;
			set
			{
				_cart = value;
				NotifyOfPropertyChange(() => Cart);
			}
		}

		public string SubTotal
		{
			get
			{
				// TODO - replace with calculation
				return "$0.00";
			}
		}

		public string Total
		{
			get
			{
				// TODO - replace with calculation
				return "$0.00";
			}
		}

		public string Tax
		{
			get
			{
				// TODO - replace with calculation
				return "$0.00";
			}
		}


		public bool CanAddToCart
		{
			get
			{
				bool output = false;

				// Make sure something is selected
				// Make sure the item has a quantity greater than 0

				return output;
			}
		}

		public bool CanRemoveFromCart
		{
			get
			{
				bool output = false;

				// Make sure something is selected

				return output;
			}
		}

		public bool CanCheckout
		{
			get
			{
				bool output = false;

				// Make sure something is in the cart

				return output;
			}
		}

		public void AddToCart()
		{

		}

		public void RemoveFromCart()
		{

		}

		public void Checkout()
		{

		}

	}
}
