using Galaxy.AcademicMagement.Application.Dto;
using Galaxy.AcademicMagement.Application.Dto.Professors;
using Galaxy.AcademicMagement.Application.Handlers.Professors.Create;
using Galaxy.AcademicMagement.Application.Handlers.Professors.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Galaxy.AcademicManagement.API.Controllers
{
    [Authorize(Roles = "Manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProfessorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllProfessorsQuery());
            return Ok(BaseResponse<ICollection<ProfessorResponse>>.Success(result));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProfessorCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(BaseResponse<Guid>.Success(result, "Professor created successfully"));
        }
    }
}
