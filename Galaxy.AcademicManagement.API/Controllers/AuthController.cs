using Galaxy.AcademicMagement.Application.Dto;
using Galaxy.AcademicMagement.Application.Dto.Login;
using Galaxy.AcademicMagement.Application.Handlers.Auth.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Galaxy.AcademicManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginQuery loginQuery)
        {
            var result = await _mediator.Send(loginQuery);
            return Ok(BaseResponse<LoginResponse>.Success(result));
        }
    }
}
