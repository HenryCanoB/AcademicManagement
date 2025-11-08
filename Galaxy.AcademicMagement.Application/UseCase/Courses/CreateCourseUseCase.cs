using Galaxy.AcademicMagement.Application.Contracts.Repositories;
using Galaxy.AcademicMagement.Application.Contracts.Services;
using Galaxy.AcademicMagement.Application.Contracts.UseCases;
using Galaxy.AcademicMagement.Application.Dto.Courses;
using Galaxy.AcademicMagement.Domain.Entities;

namespace Galaxy.AcademicMagement.Application.UseCase.Courses
{
    public class CreateCourseUseCase : ICreateCourseUseCase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IProfessorRepository _professorRepository;
        private readonly IAcademicManagementUnitOfWork _unitOfWork;

        public CreateCourseUseCase(
            ICourseRepository courseRepository,
            IProfessorRepository professorRepository,
            IAcademicManagementUnitOfWork unitOfWork)
        {
            _courseRepository = courseRepository;
            _professorRepository = professorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> ExecuteAsync(CreateCourseRequest request)
        {
            // Validate professor exists
            var professor = await _professorRepository.GetByIdAsync(request.ProfessorId);
            if (professor == null)
                throw new InvalidOperationException($"Professor with ID {request.ProfessorId} not found");

            var course = new Course(
                request.Name, 
                request.Description, 
                request.Credits, 
                request.ProfessorId);
            
            var result = await _courseRepository.AddAsync(course);

            await _unitOfWork.SaveChangesAsync();
            return result.Id;
        }
    }
}
