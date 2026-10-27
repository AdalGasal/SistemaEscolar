namespace SistemaEscolar.Shared.Helpers;

public class ValidationException : Exception
{
    public ValidationException(string message) : base(message)
    { }
}
