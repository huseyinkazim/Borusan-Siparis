using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Borusan.Api.Controllers.Base
{
	//[Authorize]
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class BaseApiContoller: ControllerBase
	{
	}
}
