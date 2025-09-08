using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task2.Application.Dtos.AuthDtos;
using Task2.Application.Services.IServices;

namespace Task2.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthServices _authServices;
        public AuthenticationController(IAuthServices authServices)
        {
            _authServices = authServices;
        }
        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] RegistrationRequestDto requestDto)
        {
            var result = await _authServices.Register(requestDto);
            if(result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Error);
        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var result = await _authServices.Login(loginRequestDto);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Error);
        }
    }
}
