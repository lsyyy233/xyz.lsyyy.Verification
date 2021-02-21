using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using xyz.lsyyy.Verification.Extension;

namespace xyz.lsyyy.Verification.Test
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class UserController : ControllerBase
	{
		[HttpGet]
		[AuthorizationTag(Name = "RegistUser")]
		public Task<object> RegistUserAsync([FromBody] UserAddModel model)
		{
			User
		}
	}
}
