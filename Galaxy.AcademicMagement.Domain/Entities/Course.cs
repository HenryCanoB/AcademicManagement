namespace Galaxy.AcademicMagement.Domain.Entities
{
    public class Course : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Credits { get; private set; }
        public Guid ProfessorId { get; private set; }
        public Professor Professor { get; private set; }
        private readonly List<Enrollment> _enrollments = new();
        public IReadOnlyCollection<Enrollment> Enrollments => _enrollments.AsReadOnly();

        protected Course() { }
        public Course(string name, string description, int credits, Guid professorId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty", nameof(name));

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be null or empty", nameof(description));

            if (credits <= 0)
                throw new ArgumentException("Credits must be greater than zero", nameof(credits));

            if (professorId == Guid.Empty)
                throw new ArgumentException("Professor ID cannot be empty", nameof(professorId));

            Name = name;
            Description = description;
            Credits = credits;
            ProfessorId = professorId;
        }

        public void AssignProfessor(Guid professorId)
        {
            if (professorId == Guid.Empty)
                throw new ArgumentException("Professor ID cannot be empty", nameof(professorId));

            ProfessorId = professorId;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
