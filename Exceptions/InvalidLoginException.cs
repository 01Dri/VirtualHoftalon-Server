namespace VirtualHoftalon_Server.Exceptions;

public class InvalidLoginException : Exception
{
    public InvalidLoginException(string msg)
    :base(msg)
    {
        
    }
}