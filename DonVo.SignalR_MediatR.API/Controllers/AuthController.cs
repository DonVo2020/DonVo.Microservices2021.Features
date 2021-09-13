using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DonVo.SignalR_MediatR.Core.Commands;
using DonVo.SignalR_MediatR.Core.Models.Requests;
using DonVo.SignalR_MediatR.Core.Models.Responses;

namespace DonVo.SignalR_MediatR.API.Controllers
{
    [AllowAnonymous]
    public class AuthController : BaseApiController
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            await _mediator.Send(new Register.Command(registerRequest.Email, registerRequest.Password));
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserResponse>> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _mediator.Send(new Login.Command(loginRequest.Email, loginRequest.Password));
            return user;
        }
    }
}
