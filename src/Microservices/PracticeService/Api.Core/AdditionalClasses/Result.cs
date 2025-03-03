namespace Api.Core.AdditionalClasses;

public class Result<T>
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }

    public Result(bool isSuccess, string message, T data = default)
    {
        IsSuccess = isSuccess;
        Message = message;
        Data = data;
    }

    public static Result<T> Success(T data, string message = "")
    {
        return new Result<T>(true, message, data);
    }

    public static Result<T> Failure(string message)
    {
        return new Result<T>(false, message);
    }
}