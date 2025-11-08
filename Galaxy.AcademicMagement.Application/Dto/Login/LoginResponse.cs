namespace Galaxy.AcademicMagement.Application.Dto.Login
{
    public class LoginResponse
    {
        public string FullName { get; set; } = default!;
        public string Role { get; set; } = default!;
        public string AccesToken { get; set; } = default!;
    }
}
