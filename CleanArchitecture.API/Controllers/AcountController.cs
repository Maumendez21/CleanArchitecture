using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AcountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AcountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody]AuthRequest request)
        {
            return Ok(await _authService.Login(request));
        }
        
        [HttpPost("Register")]
        public async Task<ActionResult<AuthResponse>> Register([FromBody]RegistationRequest request)
        {
            return Ok(await _authService.Register(request));
        }
    }
}
