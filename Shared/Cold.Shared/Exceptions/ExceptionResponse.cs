using System.Net;

namespace Cold.Shared.Exceptions;

public sealed record ExceptionResponse(object Response, HttpStatusCode StatusCode);