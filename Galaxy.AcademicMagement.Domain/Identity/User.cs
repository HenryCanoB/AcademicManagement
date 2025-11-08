using Galaxy.AcademicMagement.Domain.Exceptions;

namespace Galaxy.AcademicMagement.Domain.Identity
{
    public class User
    {
        public Guid StudentId { get; private set; }
        public Guid Id { get; private set; }
        public string FullName { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string PasswordHash { get; private set; }
        public string Role { get; private set; }

        public User() { }

        private User(Guid studentId, string fullName, string userName, string email, string password, string role)
        {
            StudentId = studentId;
            Id = Guid.NewGuid();
            FullName = fullName;
            UserName = userName;
            Email = email;
            Password = password;
            Role = role;
        }

        public static User Create(Guid studentId, string fullName, string userName, string email, string password, string role)
        {
            if (string.IsNullOrEmpty(fullName))
                throw new DomainException("fullName is required");
            if (string.IsNullOrEmpty(userName))
                throw new DomainException("userName is required");
            if (string.IsNullOrEmpty(email))
                throw new DomainException("email is required");
            if (string.IsNullOrEmpty(password))
                throw new DomainException("password is required");
            if (string.IsNullOrEmpty(role))
                throw new DomainException("role is required");

            // Here you can add any validation or business rules before creating the User
            return new User(studentId, fullName, userName, email, password, role);
        }
    }
}
