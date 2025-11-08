using Galaxy.AcademicMagement.Domain.ValueObjects;

namespace Galaxy.AcademicMagement.Domain.Entities
{
    public class Student : BaseEntity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public IdentityDocument Document { get; private set; }
        public string Email { get; private set; }

        private readonly List<Enrollment> _enrollments = new();
        public IReadOnlyCollection<Enrollment> Enrollments => _enrollments.AsReadOnly();

        protected Student() { }
        
        public Student(string firstName, string lastName, IdentityDocument document, string email)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name cannot be null or empty", nameof(firstName));

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be null or empty", nameof(lastName));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty", nameof(email));

            FirstName = firstName;
            LastName = lastName;
            Document = document ?? throw new ArgumentNullException(nameof(document));
            Email = email;
        }

        public string GetFullName() => $"{FirstName} {LastName}";

        public bool IsEnrolledInCourse(Guid courseId)
        {
            return _enrollments.Any(e => e.CourseId == courseId && e.IsActive);
        }

        public void EnrollInCourse(Enrollment enrollment)
        {
            if (enrollment == null)
                throw new ArgumentNullException(nameof(enrollment));

            if (IsEnrolledInCourse(enrollment.CourseId))
                throw new InvalidOperationException($"Student is already enrolled in course {enrollment.CourseId}");

            _enrollments.Add(enrollment);
        }
    }
}
