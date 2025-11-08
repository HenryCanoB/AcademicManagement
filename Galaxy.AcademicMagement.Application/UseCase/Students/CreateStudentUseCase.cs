using Galaxy.AcademicMagement.Application.Contracts.Repositories;
using Galaxy.AcademicMagement.Application.Contracts.Services;
using Galaxy.AcademicMagement.Application.Contracts.UseCases;
using Galaxy.AcademicMagement.Application.Dto.Students;
using Galaxy.AcademicMagement.Domain.Entities;
using Galaxy.AcademicMagement.Domain.ValueObjects;

namespace Galaxy.AcademicMagement.Application.UseCase.Students
{
    public class CreateStudentUseCase : ICreateStudentUseCase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IAcademicManagementUnitOfWork _unitOfWork;

        public CreateStudentUseCase(IStudentRepository studentRepository, IAcademicManagementUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> ExecuteAsync(CreateStudentRequest request)
        {
            var document = new IdentityDocument(request.DocumentType, request.DocumentNumber);
            var student = new Student(request.FirstName, request.LastName, document, request.Email);
            var result = await _studentRepository.AddAsync(student);

            await _unitOfWork.SaveChangesAsync();
            return result.Id;
        }
    }
}
