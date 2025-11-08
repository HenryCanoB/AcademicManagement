namespace Galaxy.AcademicMagement.Application.Dto.Students
{
    public class CreateStudentRequest
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string DocumentType { get; set; } = default!;
        public string DocumentNumber { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
