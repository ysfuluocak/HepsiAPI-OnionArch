using HepsiAPI.Application.Features.Auths.Commands.CreateRefreshToken;
using HepsiAPI.Application.Features.Auths.Commands.Login;
using HepsiAPI.Application.Features.Auths.Commands.Register;
using HepsiAPI.Application.Services.TokenHelper;
using Microsoft.AspNetCore.Mvc;

namespace HepsiAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterCommandRequest request)
        {
            var result = await Mediator.Send(request);
            SetRefreshToken(result.RefreshToken);
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginCommandRequest request)
        {
            var result = await Mediator.Send(request);
            SetRefreshToken(result.RefreshToken);
            return Ok(result);
        }

        [HttpGet("RefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var request = new RefreshTokenCommandRequest();
            var result = await Mediator.Send(request);
            SetRefreshToken(result.RefreshToken);
            return Ok(result);
        }

        private void SetRefreshToken(RefreshToken refreshToken)
        {
            var cookieOptions = new CookieOptions()
            {
                HttpOnly = true,
                Expires = refreshToken.Expiration
            };

            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
        }
    }
}
