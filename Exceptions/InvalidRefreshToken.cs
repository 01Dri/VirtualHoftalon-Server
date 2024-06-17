namespace VirtualHoftalon_Server.Exceptions;

public class InvalidRefreshToken : Exception
{
    public InvalidRefreshToken(string msg)
    :base(msg)
    {
        
    }
    
}