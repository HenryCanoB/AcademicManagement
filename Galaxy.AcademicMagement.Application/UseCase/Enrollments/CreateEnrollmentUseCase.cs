using Galaxy.AcademicMagement.Application.Contracts.Repositories;
using Galaxy.AcademicMagement.Application.Contracts.Services;
using Galaxy.AcademicMagement.Application.Contracts.UseCases;
using Galaxy.AcademicMagement.Application.Dto.Enrollments;
using Galaxy.AcademicMagement.Domain.Entities;

namespace Galaxy.AcademicMagement.Application.UseCase.Enrollments
{
    public class CreateEnrollmentUseCase : ICreateEnrollmentUseCase
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IAcademicManagementUnitOfWork _unitOfWork;

        public CreateEnrollmentUseCase(
            IEnrollmentRepository enrollmentRepository,
            IStudentRepository studentRepository,
            ICourseRepository courseRepository,
            IAcademicManagementUnitOfWork unitOfWork)
        {
            _enrollmentRepository = enrollmentRepository;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> ExecuteAsync(CreateEnrollmentRequest request)
        {
            // Validate student exists
            var student = await _studentRepository.GetByIdAsync(request.StudentId);
            if (student == null)
                throw new InvalidOperationException($"Student with ID {request.StudentId} not found");

            // Validate course exists
            var course = await _courseRepository.GetByIdAsync(request.CourseId);
            if (course == null)
                throw new InvalidOperationException($"Course with ID {request.CourseId} not found");

            // Check if already enrolled
            var existingEnrollment = await _enrollmentRepository.GetEnrollmentByStudentAndCourseAsync(request.StudentId, request.CourseId);
            if (existingEnrollment != null)
                throw new InvalidOperationException($"Student is already enrolled in this course");

            var enrollment = new Enrollment(request.StudentId, request.CourseId);
            var result = await _enrollmentRepository.AddAsync(enrollment);

            await _unitOfWork.SaveChangesAsync();
            return result.Id;
        }
    }
}
