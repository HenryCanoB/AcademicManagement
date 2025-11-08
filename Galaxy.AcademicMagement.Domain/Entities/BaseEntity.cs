namespace Galaxy.AcademicMagement.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; protected set; }

        public bool IsActive { get; protected set; }

        public DateTime CreatedAt { get; protected set; }

        public DateTime UpdatedAt { get; protected internal set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

    }
}
