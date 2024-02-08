using System.Diagnostics;

namespace PdfGenerator.Shared;

/// <summary>
///     Represents the result of an operation without a return value.
/// </summary>
[DebuggerDisplay("IsSuccess: {IsSuccess}, ErrorMessage: {ErrorMessage}")]
public class Result
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Result" /> class.
    /// </summary>
    /// <param name="isSuccess">Pass <c>true</c> if the operation was successful.</param>
    /// <param name="errorMessage">The error message.</param>
    public Result(bool isSuccess, string? errorMessage = null)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }

    /// <summary>
    ///     Cached successful result.
    /// </summary>
    public static Result Success { get; } = NewSuccess();

    /// <summary>
    ///     Cached failure result.
    /// </summary>
    public static Result Failure { get; } = NewFailure();

    /// <summary>
    ///     Gets a value indicating whether the operation was successful.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    ///     Gets the error message if the operation failed.
    /// </summary>
    /// <value>
    ///     The error message string or null if the operation was successful.
    /// </value>
    public string? ErrorMessage { get; }

    /// <summary>
    ///     Creates a successful result of an operation without a return value.
    /// </summary>
    /// <returns>A result indicating success.</returns>
    public static Result NewSuccess()
    {
        return new Result(true);
    }

    /// <summary>
    ///     Creates a failed result of an operation without a return value.
    /// </summary>
    /// <param name="errorMessage">The error message.</param>
    /// <returns>A result indicating failure.</returns>
    public static Result NewFailure(string? errorMessage = null)
    {
        return new Result(false, errorMessage);
    }
}
