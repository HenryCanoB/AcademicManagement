namespace Galaxy.AcademicMagement.Application.Dto.Courses
{
    public class CourseResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int Credits { get; set; }
        public Guid ProfessorId { get; set; }
        public string ProfessorName { get; set; } = default!;
        public bool IsActive { get; set; }
    }
}
