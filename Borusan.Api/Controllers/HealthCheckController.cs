using Borusan.Api.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Borusan.Api.Controllers
{

	public class HealthCheckController: BaseApiContoller
	{
		public HealthCheckController()
		{
		}
		[HttpGet()]
		[AllowAnonymous]
		public string IsOk() { return "OK"; }
	}
}
