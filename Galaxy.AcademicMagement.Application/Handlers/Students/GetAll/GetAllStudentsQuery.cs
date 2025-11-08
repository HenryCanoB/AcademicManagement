using Galaxy.AcademicMagement.Application.Dto.Students;
using MediatR;

namespace Galaxy.AcademicMagement.Application.Handlers.Students.GetAll
{
    public record GetAllStudentsQuery() : IRequest<ICollection<StudentResponse>>;
}
