using Galaxy.AcademicMagement.Application.Dto.Login;
using MediatR;

namespace Galaxy.AcademicMagement.Application.Handlers.Auth.Login
{
    public record LoginQuery(string UserName, string Password) : IRequest<LoginResponse>;
}
