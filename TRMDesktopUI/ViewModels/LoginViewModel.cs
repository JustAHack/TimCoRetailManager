using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDesktopUI.EventModels;
using TRMDesktopUI.Library.Api;

namespace TRMDesktopUI.ViewModels
{
	public class LoginViewModel : Screen
	{
		private string _userName = "tkauran@gmail.com";
		private string _password = "Pwd12345.";
		private IAPIHelper _apiHelper;
		private IEventAggregator _events;

		public LoginViewModel(IAPIHelper apiHelper, IEventAggregator events)
		{
			_apiHelper = apiHelper;
			_events = events;
		}

		public string UserName
		{
			get => _userName;
			set
			{
				_userName = value;
				NotifyOfPropertyChange(() => UserName);
				NotifyOfPropertyChange(() => CanLogIn);
			}
		}

		public string Password
		{
			get => _password;
			set
			{
				_password = value;
				NotifyOfPropertyChange(() => Password);
				NotifyOfPropertyChange(() => CanLogIn);
			}
		}


		public bool IsErrorVisible
		{
			get
			{
				bool output = false;
				if (ErrorMessage?.Length > 0)
				{
					output = true;
				}
				return output;
			}
		}

		private string _errorMessage;

		public string ErrorMessage
		{
			get => _errorMessage;
			set
			{
				_errorMessage = value;
				NotifyOfPropertyChange(() => IsErrorVisible);
				NotifyOfPropertyChange(() => ErrorMessage);
			}
		}

		public bool CanLogIn
		{
			get
			{
				bool result = false;
				if (UserName?.Length > 0 && Password?.Length > 0)
				{
					result = true;
				}

				return result;
			}
		}

		public async Task LogIn()
		{
			try
			{
				// Reset the error messages when attempting a new login
				ErrorMessage = "";
				var result = await _apiHelper.Authenticate(UserName, Password);

				// Capture more user information
				await _apiHelper.GetLoggedInUserInfo(result.Access_Token);

				await _events.PublishOnUIThreadAsync(new LogOnEvent());
			}
			catch (Exception ex)
			{
				ErrorMessage = ex.Message;
			}
		}

	}
}
