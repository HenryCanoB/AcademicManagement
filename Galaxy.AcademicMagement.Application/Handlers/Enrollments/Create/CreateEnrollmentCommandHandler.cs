using Galaxy.AcademicMagement.Application.Contracts.Repositories;
using Galaxy.AcademicMagement.Application.Contracts.Services;
using Galaxy.AcademicMagement.Domain.Entities;
using MediatR;

namespace Galaxy.AcademicMagement.Application.Handlers.Enrollments.Create
{
    public class CreateEnrollmentCommandHandler : IRequestHandler<CreateEnrollmentCommand, Guid>
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IAcademicManagementUnitOfWork _unitOfWork;

        public CreateEnrollmentCommandHandler(
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

        public async Task<Guid> Handle(CreateEnrollmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                // Validate student exists
                var student = await _studentRepository.GetByIdAsync(request.StudentId);
                if (student == null)
                    throw new ApplicationException($"Student with ID {request.StudentId} not found");

                // Validate course exists
                var course = await _courseRepository.GetByIdAsync(request.CourseId);
                if (course == null)
                    throw new ApplicationException($"Course with ID {request.CourseId} not found");

                // Check if already enrolled
                var existingEnrollment = await _enrollmentRepository.GetEnrollmentByStudentAndCourseAsync(
                    request.StudentId, 
                    request.CourseId);
                    
                if (existingEnrollment != null && existingEnrollment.IsActive)
                    throw new ApplicationException("Student is already enrolled in this course");

                var enrollment = new Enrollment(request.StudentId, request.CourseId);
                var result = await _enrollmentRepository.AddAsync(enrollment);

                await _unitOfWork.CommitTransactionAsync();
                return result.Id;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
