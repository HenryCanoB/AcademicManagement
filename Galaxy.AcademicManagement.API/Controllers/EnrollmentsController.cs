using Galaxy.AcademicMagement.Application.Dto;
using Galaxy.AcademicMagement.Application.Dto.Enrollments;
using Galaxy.AcademicMagement.Application.Handlers.Enrollments.Create;
using Galaxy.AcademicMagement.Application.Handlers.Enrollments.GetAll;
using Galaxy.AcademicMagement.Application.Handlers.Enrollments.GetByCourse;
using Galaxy.AcademicMagement.Application.Handlers.Enrollments.GetByStudent;
using Galaxy.AcademicMagement.Application.Handlers.Enrollments.Withdraw;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Galaxy.AcademicManagement.API.Controllers
{
    [Authorize(Roles = "Manager,Student")]
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EnrollmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllEnrollmentsQuery());
            return Ok(BaseResponse<ICollection<EnrollmentResponse>>.Success(result));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEnrollmentCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(BaseResponse<Guid>.Success(result, "Enrollment created successfully"));
        }

        [HttpPatch("{enrollmentId}/withdraw")]
        public async Task<IActionResult> Withdraw(Guid enrollmentId)
        {
            var command = new WithdrawEnrollmentCommand(enrollmentId);
            var result = await _mediator.Send(command);
            return Ok(BaseResponse<bool>.Success(result, "Enrollment withdrawn successfully"));
        }

        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetByStudent(Guid studentId)
        {
            var query = new GetEnrollmentsByStudentQuery(studentId);
            var result = await _mediator.Send(query);
            return Ok(BaseResponse<ICollection<EnrollmentResponse>>.Success(result));
        }

        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetByCourse(Guid courseId)
        {
            var query = new GetEnrollmentsByCourseQuery(courseId);
            var result = await _mediator.Send(query);
            return Ok(BaseResponse<ICollection<EnrollmentResponse>>.Success(result));
        }
    }
}
