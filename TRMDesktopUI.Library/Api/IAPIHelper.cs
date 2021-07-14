using System.Net.Http;
using System.Threading.Tasks;
using TRMDesktopUI.Library.Models;

namespace TRMDesktopUI.Library.Api
{
	public interface IAPIHelper
	{
		HttpClient ApiClient { get; }
		Task<AuthenticatedUser> Authenticate(string userName, string password);
		void LogOffUser();
		Task GetLoggedInUserInfo(string token);
	}
}