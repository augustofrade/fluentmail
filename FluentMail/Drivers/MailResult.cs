namespace FluentMail.Drivers;

public class MailResult
{
    private MailResult(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }
    
    public string Message { get; }
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    
    public static MailResult Success() => new(true, string.Empty);

    public static MailResult Failure(string message) => new(false, message);
}
