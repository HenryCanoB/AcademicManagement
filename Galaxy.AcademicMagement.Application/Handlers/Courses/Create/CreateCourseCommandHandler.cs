using Galaxy.AcademicMagement.Application.Contracts.Repositories;
using Galaxy.AcademicMagement.Application.Contracts.Services;
using Galaxy.AcademicMagement.Domain.Entities;
using MediatR;

namespace Galaxy.AcademicMagement.Application.Handlers.Courses.Create
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Guid>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IProfessorRepository _professorRepository;
        private readonly IAcademicManagementUnitOfWork _unitOfWork;

        public CreateCourseCommandHandler(
            ICourseRepository courseRepository,
            IProfessorRepository professorRepository,
            IAcademicManagementUnitOfWork unitOfWork)
        {
            _courseRepository = courseRepository;
            _professorRepository = professorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                // Validate professor exists
                var professor = await _professorRepository.GetByIdAsync(request.ProfessorId);
                if (professor == null)
                    throw new ApplicationException($"Professor with ID {request.ProfessorId} not found");

                var course = new Course(
                    request.Name,
                    request.Description,
                    request.Credits,
                    request.ProfessorId);

                var result = await _courseRepository.AddAsync(course);

                if (result == null)
                {
                    throw new ApplicationException("Error creating course.");
                }

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
