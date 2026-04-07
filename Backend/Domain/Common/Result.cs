namespace Backend.Domain.Common;

/// <summary>
/// Representa el resultado de una operación sin valor de retorno.
/// </summary>
public class Result
{
    public bool IsSuccess { get; }
    public string? Error { get; }

    protected Result(bool isSuccess, string? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    /// <summary>
    /// Crea un resultado exitoso.
    /// </summary>
    public static Result Success() => new(true, null);
    /// <summary>
    /// Crea un resultado fallido con mensaje de error.
    /// </summary>
    public static Result Failure(string error) => new(false, error);
}

/// <summary>
/// Representa el resultado de una operación con valor de retorno.
/// </summary>
public class Result<T> : Result
{
    public T? Value { get; }

    private Result(bool isSuccess, T? value, string? error) : base(isSuccess, error)
    {
        Value = value;
    }

    /// <summary>
    /// Crea un resultado exitoso con valor.
    /// </summary>
    public static Result<T> Success(T value) => new(true, value, null);
    /// <summary>
    /// Crea un resultado fallido con mensaje de error.
    /// </summary>
    public static new Result<T> Failure(string error) => new(false, default, error);
}
