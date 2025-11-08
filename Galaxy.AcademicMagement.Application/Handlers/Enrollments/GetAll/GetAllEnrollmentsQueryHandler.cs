using Galaxy.AcademicMagement.Application.Contracts.Repositories;
using Galaxy.AcademicMagement.Application.Dto.Enrollments;
using MediatR;

namespace Galaxy.AcademicMagement.Application.Handlers.Enrollments.GetAll
{
    public class GetAllEnrollmentsQueryHandler : IRequestHandler<GetAllEnrollmentsQuery, ICollection<EnrollmentResponse>>
    {
        
        private readonly IEnrollmentRepository _enrollmentRepository;

        public GetAllEnrollmentsQueryHandler(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        public async Task<ICollection<EnrollmentResponse>> Handle(GetAllEnrollmentsQuery request, CancellationToken cancellationToken)
        {
            var enrollments = await _enrollmentRepository.GetAllWithRelationsAsync();

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
