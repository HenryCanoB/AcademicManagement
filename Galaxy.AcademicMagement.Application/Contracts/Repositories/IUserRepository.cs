using Galaxy.AcademicMagement.Domain.Identity;

namespace Galaxy.AcademicMagement.Application.Contracts.Repositories
{
    public interface IUserRepository
    {
        Task<(bool Success, IEnumerable<string> Errors)> CreateAsync(User user);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByUserNameAsync(string userName);
        Task<bool> CheckPassowrdAsync(User user, string password);
        Task<List<string>> GetRolesAsync(User user);
    }
}
