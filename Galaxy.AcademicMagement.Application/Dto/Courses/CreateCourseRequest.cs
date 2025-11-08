namespace Galaxy.AcademicMagement.Application.Dto.Courses
{
    public class CreateCourseRequest
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int Credits { get; set; }
        public Guid ProfessorId { get; set; }
    }
}
