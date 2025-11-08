using Galaxy.AcademicMagement.Application.Contracts.Repositories;
using Galaxy.AcademicMagement.Application.Dto.Students;
using Mapster;
using MediatR;

namespace Galaxy.AcademicMagement.Application.Handlers.Students.GetAll
{
    public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, ICollection<StudentResponse>>
    {
        private readonly IStudentRepository _studentRepository;

        public GetAllStudentsQueryHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<ICollection<StudentResponse>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var students = await _studentRepository.ListAsync();
            var response = students.Select(s => new StudentResponse
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                FullName = s.GetFullName(),
                DocumentType = s.Document.Type,
                DocumentNumber = s.Document.Number,
                Email = s.Email,
                IsActive = s.IsActive
            }).ToList();

            return response;
        }
    }
}
