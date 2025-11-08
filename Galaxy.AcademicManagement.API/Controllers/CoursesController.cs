using Galaxy.AcademicMagement.Application.Dto;
using Galaxy.AcademicMagement.Application.Dto.Courses;
using Galaxy.AcademicMagement.Application.Handlers.Courses.Create;
using Galaxy.AcademicMagement.Application.Handlers.Courses.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Galaxy.AcademicManagement.API.Controllers
{
    [Authorize(Roles = "Manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CoursesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCoursesQuery());
            return Ok(BaseResponse<ICollection<CourseResponse>>.Success(result));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCourseCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(BaseResponse<Guid>.Success(result, "Course created successfully"));
        }
    }
}
