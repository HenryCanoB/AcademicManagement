using Galaxy.AcademicMagement.Application.Contracts.Repositories;
using Galaxy.AcademicMagement.Application.Dto.Enrollments;
using MediatR;

namespace Galaxy.AcademicMagement.Application.Handlers.Enrollments.GetByCourse
{
    public class GetEnrollmentsByCourseQueryHandler : IRequestHandler<GetEnrollmentsByCourseQuery, ICollection<EnrollmentResponse>>
    {
        private readonly IEnrollmentRepository _enrollmentRepository;

        public GetEnrollmentsByCourseQueryHandler(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        public async Task<ICollection<EnrollmentResponse>> Handle(GetEnrollmentsByCourseQuery request, CancellationToken cancellationToken)
        {
            var enrollments = await _enrollmentRepository.GetEnrollmentsByCourseAsync(request.CourseId);

            var response = enrollments.Select(e => new EnrollmentResponse
            {
                Id = e.Id,
                StudentId = e.StudentId,
                StudentName = e.Student != null ? e.Student.GetFullName() : string.Empty,
                CourseId = e.CourseId,
                CourseName = e.Course != null ? e.Course.Name : string.Empty,
                EnrollmentDate = e.EnrollmentDate,
                Condition = e.Condition.ToString(),
                IsActive = e.IsActive
            }).ToList();

            return response;
        }
    }
}
