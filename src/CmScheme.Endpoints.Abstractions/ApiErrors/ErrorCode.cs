namespace CmScheme.Endpoints.Abstractions.ApiErrors;

public sealed record ErrorCode
{
    public static readonly ErrorCode Validation = new("validation_failure");
    public static readonly ErrorCode Conflict = new("conflict");
    public static readonly ErrorCode NotFound = new("not_found");
    public static readonly ErrorCode Unauthorized = new("unauthorized");
    public static readonly ErrorCode ServerError = new("server_error");
    public static readonly ErrorCode Forbidden = new("forbidden");
    public static readonly ErrorCode BadRequest = new("bad_request");

    private ErrorCode(string code) { Code = code; }
    public string Code { get; }
}
