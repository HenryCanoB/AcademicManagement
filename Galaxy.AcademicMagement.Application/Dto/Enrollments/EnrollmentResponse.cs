namespace Galaxy.AcademicMagement.Application.Dto.Enrollments
{
    public class EnrollmentResponse
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string StudentName { get; set; } = default!;
        public Guid CourseId { get; set; }
        public string CourseName { get; set; } = default!;
        public DateTime EnrollmentDate { get; set; }
        public string Condition { get; set; } = default!;
        public bool IsActive { get; set; }
    }
}
