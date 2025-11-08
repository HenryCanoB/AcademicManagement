using Galaxy.AcademicMagement.Domain.Identity;

namespace Galaxy.AcademicMagement.Application.Contracts.Services
{
    public interface IAuthenticationService
    {
        string GenerateToken(User request, string role);
    }
}
