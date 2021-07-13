﻿using AutoMapper;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TRMDesktopUI.Library.Api;
using TRMDesktopUI.Library.Helpers;
using TRMDesktopUI.Library.Models;
using TRMDesktopUI.Models;
using TRMDesktopUI.ViewModels;

// https://caliburnmicro.com/documentation/bootstrapper
// https://caliburnmicro.com/documentation/simple-container

namespace TRMDesktopUI
{
	public class Bootstrapper : BootstrapperBase
	{

		private SimpleContainer _container = new SimpleContainer();

		public Bootstrapper()
		{
			Initialize();

			_ = ConventionManager.AddElementConvention<PasswordBox>(
				PasswordBoxHelper.BoundPasswordProperty,
				"Password",
				"PasswordChanged");
		}

		private IMapper ConfigureAutomapper()
		{
			var Config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<ProductModel, ProductDisplayModel>();
				cfg.CreateMap<CartItemModel, CartItemDisplayModel>();
			});

			var mapper = Config.CreateMapper();

			return mapper;
		}

		protected override void Configure()
		{
			_ = _container.Instance(ConfigureAutomapper());

			_ = _container.Instance(_container)
				.PerRequest<ISaleEndpoint, SaleEndpoint>()
				.PerRequest<IProductEndpoint, ProductEndpoint>();

			_ = _container
				.Singleton<IWindowManager, WindowManager>()
				.Singleton<IEventAggregator, EventAggregator>()
				.Singleton<ILoggedInUserModel, LoggedInUserModel>()
				.Singleton<IConfigHelper, ConfigHelper>()
				.Singleton<IAPIHelper, APIHelper>();

			GetType().Assembly.GetTypes()
				.Where(type => type.IsClass)
				.Where(type => type.Name.EndsWith("ViewModel"))
				.ToList()
				.ForEach(viewModelType => _container.RegisterPerRequest(
					viewModelType, viewModelType.ToString(), viewModelType));
		}

		protected override void OnStartup(object sender, StartupEventArgs e)
		{
			DisplayRootViewFor<ShellViewModel>();
		}

		protected override object GetInstance(Type service, string key)
		{
			return _container.GetInstance(service, key);
		}

		protected override IEnumerable<object> GetAllInstances(Type service)
		{
			return _container.GetAllInstances(service);
		}

		protected override void BuildUp(object instance)
		{
			_container.BuildUp(instance);
		}
	}
}
