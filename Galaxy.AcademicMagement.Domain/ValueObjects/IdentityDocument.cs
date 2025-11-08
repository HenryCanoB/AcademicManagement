namespace Galaxy.AcademicMagement.Domain.ValueObjects
{
    public class IdentityDocument
    {
        public string Type { get; private set; }
        public string Number { get; private set; }
        public IdentityDocument() { }
        public IdentityDocument(string type, string number)
        {
            if (string.IsNullOrEmpty(type))
                throw new ArgumentException("Type cannot be null or empty", nameof(type));
            if (string.IsNullOrEmpty(number))
                throw new ArgumentException("Number cannot be null or empty", nameof(number));
            Type = type;
            Number = number;
        }

        public override bool Equals(object? obj)
        => (obj is IdentityDocument document && Type == document.Type && Number == document.Number);

        public override int GetHashCode() => HashCode.Combine(Type, Number);

    }
}