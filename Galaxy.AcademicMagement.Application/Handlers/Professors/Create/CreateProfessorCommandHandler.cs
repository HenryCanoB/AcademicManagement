using Galaxy.AcademicMagement.Application.Contracts.Repositories;
using Galaxy.AcademicMagement.Application.Contracts.Services;
using Galaxy.AcademicMagement.Domain.Entities;
using Galaxy.AcademicMagement.Domain.ValueObjects;
using MediatR;

namespace Galaxy.AcademicMagement.Application.Handlers.Professors.Create
{
    public class CreateProfessorCommandHandler : IRequestHandler<CreateProfessorCommand, Guid>
    {
        private readonly IProfessorRepository _professorRepository;
        private readonly IAcademicManagementUnitOfWork _unitOfWork;

        public CreateProfessorCommandHandler(
            IProfessorRepository professorRepository,
            IAcademicManagementUnitOfWork unitOfWork)
        {
            _professorRepository = professorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateProfessorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                // Validate if professor with same document number already exists
                var existingProfessor = await _professorRepository.GetByDocumentNumberAsync(request.DocumentNumber);
                if (existingProfessor != null)
                {
                    throw new ApplicationException($"Ya existe un profesor con el número de documento {request.DocumentNumber}.");
                }

                var document = new IdentityDocument(request.DocumentType, request.DocumentNumber);
                var professor = new Professor(
                    request.FirstName,
                    request.LastName,
                    document,
                    request.Email,
                    request.Specialization);

                var result = await _professorRepository.AddAsync(professor);

                if (result == null)
                {
                    throw new ApplicationException("Error creating professor.");
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
