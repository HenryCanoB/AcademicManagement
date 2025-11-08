using Galaxy.AcademicMagement.Application.Contracts.Repositories;
using Galaxy.AcademicMagement.Application.Contracts.Services;
using Galaxy.AcademicMagement.Application.Dto.Login;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Galaxy.AcademicMagement.Application.Handlers.Auth.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<LoginQueryHandler> _logger;

        public LoginQueryHandler(
            IUserRepository userRepository, 
            IAuthenticationService authenticationService, 
            ILogger<LoginQueryHandler> logger)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
            _logger = logger;
        }

        public async Task<LoginResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUserNameAsync(request.UserName);

            if (user is null)
            {
                throw new ApplicationException("Invalid username or password.");
            }

            var password = await _userRepository.CheckPassowrdAsync(user, request.Password);

            if (!password)
            {
                throw new ApplicationException("Invalid username or password.");
            }

            var roles = await _userRepository.GetRolesAsync(user);
            if (roles is null || !roles.Any())
            {
                throw new ApplicationException($"El usuario {request.UserName} no tiene un rol asignado.");
            }

            var role = roles.First();

            var token = _authenticationService.GenerateToken(user, role);

            _logger.LogInformation($"User {request.UserName} logged in successfully.");

            return new()
            {
                AccesToken = token,
                FullName = user.FullName,
                Role = role
            };
        }
    }
}
