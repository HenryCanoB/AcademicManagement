using Galaxy.AcademicMagement.Domain.ValueObjects;

namespace Galaxy.AcademicMagement.Domain.Entities
{
    public class Professor : BaseEntity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public IdentityDocument Document { get; private set; }
        public string Email { get; private set; }
        public string Specialization { get; private set; }

        private readonly List<Course> _courses = new();
        public IReadOnlyCollection<Course> Courses => _courses.AsReadOnly();

        protected Professor() { }

        public Professor(string firstName, string lastName, IdentityDocument document, string email, string specialization)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name cannot be null or empty", nameof(firstName));
            
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be null or empty", nameof(lastName));
            
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty", nameof(email));
            
            if (string.IsNullOrWhiteSpace(specialization))
                throw new ArgumentException("Specialization cannot be null or empty", nameof(specialization));
            
            FirstName = firstName;
            LastName = lastName;
            Document = document ?? throw new ArgumentNullException(nameof(document));
            Email = email;
            Specialization = specialization;
        }

        public string GetFullName() => $"{FirstName} {LastName}";

    }
}
