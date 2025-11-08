using Galaxy.AcademicMagement.Application.Dto;
using Galaxy.AcademicMagement.Application.Dto.Students;
using Galaxy.AcademicMagement.Application.Handlers.Students.Create;
using Galaxy.AcademicMagement.Application.Handlers.Students.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Galaxy.AcademicManagement.API.Controllers
{
    [Authorize]
    [Authorize(Roles = "Manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllStudentsQuery());
            return Ok(BaseResponse<ICollection<StudentResponse>>.Success(result));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStudentCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(BaseResponse<Guid>.Success(result, "Student created successfully"));
        }
    }
}
