using Galaxy.AcademicMagement.Application.Contracts.Repositories;
using Galaxy.AcademicMagement.Application.Contracts.Services;
using Galaxy.AcademicMagement.Application.Contracts.UseCases;
using Galaxy.AcademicMagement.Application.Dto.Professors;
using Galaxy.AcademicMagement.Domain.Entities;
using Galaxy.AcademicMagement.Domain.ValueObjects;

namespace Galaxy.AcademicMagement.Application.UseCase.Professors
{
    public class CreateProfessorUseCase : ICreateProfessorUseCase
    {
        private readonly IProfessorRepository _professorRepository;
        private readonly IAcademicManagementUnitOfWork _unitOfWork;

        public CreateProfessorUseCase(IProfessorRepository professorRepository, IAcademicManagementUnitOfWork unitOfWork)
        {
            _professorRepository = professorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> ExecuteAsync(CreateProfessorRequest request)
        {
            var document = new IdentityDocument(request.DocumentType, request.DocumentNumber);
            var professor = new Professor(
                request.FirstName, 
                request.LastName, 
                document, 
                request.Email, 
                request.Specialization);
            
            var result = await _professorRepository.AddAsync(professor);

            await _unitOfWork.SaveChangesAsync();
            return result.Id;
        }
    }
}
