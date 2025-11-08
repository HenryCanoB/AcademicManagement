namespace Galaxy.AcademicMagement.Domain.Dpo
{
    public class OperationResult
    {
        public bool Success { get; }
        public IEnumerable<string> Errors { get; }

        private OperationResult(bool success, IEnumerable<string> errors)
        {
            Success = success;
            Errors = errors;
        }

        public static OperationResult Ok() => new(true, Enumerable.Empty<string>());

        public static OperationResult Fail(IEnumerable<string> errors) => new(false, errors);
    }
}
