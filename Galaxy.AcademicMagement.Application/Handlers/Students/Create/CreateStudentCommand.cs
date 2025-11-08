using MediatR;

namespace Galaxy.AcademicMagement.Application.Handlers.Students.Create
{
    public record CreateStudentCommand(
        string FirstName, 
        string LastName, 
        string DocumentType, 
        string DocumentNumber, 
        string Email, 
        string UserName, 
        string Password) : IRequest<Guid>;
}
