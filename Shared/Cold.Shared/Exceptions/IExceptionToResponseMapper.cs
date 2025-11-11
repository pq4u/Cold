namespace Cold.Shared.Exceptions;

internal interface IExceptionToResponseMapper
{
    ExceptionResponse Map(Exception exception);
}