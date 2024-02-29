using Microsoft.AspNetCore.Mvc;

namespace Role_Permissition_Based_Authorisation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IAuthService _authService;

        public IdentityController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public IActionResult Login(string login, string password)
        {
            var result = _authService.GenerateToken(login, password);
            return Ok(result.Result);
        }
    }
}
