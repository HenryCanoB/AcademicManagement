namespace Galaxy.AcademicMagement.Application.Dto.Enrollments
{
    public class CreateEnrollmentRequest
    {
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
    }
}
