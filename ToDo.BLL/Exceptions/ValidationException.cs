namespace ToDo.BLL.Exceptions;
public class ValidationException : Exception
{
    public ValidationException() 
    { }

    public ValidationException(string message) : base(message)
    {
    }
}