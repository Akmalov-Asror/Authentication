namespace Teaching.V1.Auth.Services.Exceptions;

public class UserException : Exception
{
    public int Code { get; set; }
    public bool? Global { get; set; }
    public UserException(int code, string message, bool? global = true) : base(message)
    {
        Code = code;
        Global = global;
    }
}
