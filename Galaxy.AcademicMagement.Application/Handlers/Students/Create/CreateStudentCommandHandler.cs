using Galaxy.AcademicMagement.Application.Contracts.Repositories;
using Galaxy.AcademicMagement.Application.Contracts.Services;
using Galaxy.AcademicMagement.Domain.Entities;
using Galaxy.AcademicMagement.Domain.Identity;
using Galaxy.AcademicMagement.Domain.ValueObjects;
using MediatR;

namespace Galaxy.AcademicMagement.Application.Handlers.Students.Create
{
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Guid>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IAcademicManagementUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public CreateStudentCommandHandler(
            IStudentRepository studentRepository, 
            IAcademicManagementUnitOfWork unitOfWork, 
            IUserRepository userRepository)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var document = new IdentityDocument(request.DocumentType, request.DocumentNumber);
                var student = new Student(request.FirstName, request.LastName, document, request.Email);
                var result = await _studentRepository.AddAsync(student);

                if (result == null)
                {
                    throw new ApplicationException("Error creating student.");
                }

                var user = User.Create(
                    result.Id, 
                    $"{request.FirstName} {request.LastName}", 
                    request.UserName, 
                    request.Email, 
                    request.Password, 
                    "Student");

                var resultUser = await _userRepository.CreateAsync(user);

                if (!resultUser.Success)
                {
                    throw new ApplicationException($"Error creating user. {string.Join("/", resultUser.Errors)}");
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
