using Galaxy.AcademicMagement.Application.Contracts.Repositories;
using Galaxy.AcademicMagement.Domain.Dpo;
using Galaxy.AcademicMagement.Domain.Identity;
using Galaxy.AcademicMagement.Infrastructure.Configurations.Entities.IdentityContext;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace Galaxy.AcademicMagement.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<UserExtIdentity> _userManager;

        public UserRepository(UserManager<UserExtIdentity> userManager)
        {
            _userManager = userManager;
        }

        public async Task<(bool Success, IEnumerable<string> Errors)> CreateAsync(User user)
        {
            var userIdentity = user.Adapt<UserExtIdentity>();
            var result = await _userManager.CreateAsync(userIdentity, user.Password);

            if (result.Succeeded) 
                await _userManager.AddToRoleAsync(userIdentity, user.Role);

            return result.Succeeded
                ? (true, Enumerable.Empty<string>())
                : (false, result.Errors.Select(e => e.Description));
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var result = await _userManager.FindByEmailAsync(email);
            return result?.Adapt<User>();
        }

        public async Task<User?> GetByUserNameAsync(string userName)
        {
            var result = await _userManager.FindByNameAsync(userName);
            return result?.Adapt<User>();
        }

        public async Task<bool> CheckPassowrdAsync(User user, string password)
        {
            var userExt = user.Adapt<UserExtIdentity>();

            return await _userManager.CheckPasswordAsync(userExt, password);
        }

        public async Task<List<string>> GetRolesAsync(User user)
        {
            var userExt = user.Adapt<UserExtIdentity>();

            var result = await _userManager.GetRolesAsync(userExt);
            return result.ToList();

        }
    }
}
