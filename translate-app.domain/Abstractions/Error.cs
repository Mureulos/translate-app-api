namespace translate_app.Domain.Abstractions
{
    public enum ErrorType
    {
        Failure = 0,
        Validation = 1,
        NotFound = 2,
        Conflict = 3,
        Unauthorized = 4
    }

    public record Error(string Code, string Message, ErrorType Type = ErrorType.Failure)
    {
        public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);
        public static readonly Error NullValue = new("Error.NullValue", "A null value was provided.", ErrorType.Failure);
        public static readonly Error InvalidCredentials = new("Auth.InvalidCredentials", "Invalid credentials.", ErrorType.Unauthorized);
    }
}