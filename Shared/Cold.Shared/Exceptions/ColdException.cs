namespace Cold.Shared.Exceptions;

public abstract class ColdException : Exception
{
    protected ColdException(string message) : base(message)
    {
    }
}