using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Particle.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> Login()
		{
			return Ok("Login successful");
		}

	}
}
