namespace PdfGenerator.Shared;

/// <summary>
///     Represents the result of an operation with a return value.
/// </summary>
/// <typeparam name="TResult">The type of the operation's return value.</typeparam>
public class Result<TResult> : Result
{
    /// <summary>
    ///     Cached failure result.
    /// </summary>
    public new static readonly Result<TResult> Failure = NewFailure();

    /// <summary>
    ///     Initializes a new instance of the <see cref="Result{TResult}" /> class.
    /// </summary>
    /// <param name="isSuccess">Pass <c>true</c> if the operation was successful.</param>
    /// <param name="value">The value returned by the operation.</param>
    /// <param name="errorMessage">The error message.</param>
    public Result(bool isSuccess, TResult? value = default, string? errorMessage = null)
        : base(isSuccess, errorMessage)
    {
        Value = value;
    }

    /// <summary>
    ///     Gets the value returned by the operation if it was successful.
    /// </summary>
    public TResult? Value { get; }

    /// <summary>
    ///     Creates a successful result of an operation with a return value.
    /// </summary>
    /// <param name="result">The value that the result should return.</param>
    /// <returns>A result of the operation with a return value, indicating success.</returns>
    public static Result<TResult> NewSuccess(TResult result)
    {
        return new Result<TResult>(true, result);
    }

    /// <summary>
    ///     Creates a failed result of an operation with a typed value.
    /// </summary>
    /// <param name="errorMessage">The error message.</param>
    /// <returns>A result of the operation, indicating failure.</returns>
    public new static Result<TResult> NewFailure(string? errorMessage = null)
    {
        return new Result<TResult>(false, default, errorMessage);
    }
}
