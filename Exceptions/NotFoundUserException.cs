namespace VirtualHoftalon_Server.Exceptions;

public class NotFoundUserException : Exception

{

    public NotFoundUserException(string msg)
    :base(msg)
    {
        
    }
}