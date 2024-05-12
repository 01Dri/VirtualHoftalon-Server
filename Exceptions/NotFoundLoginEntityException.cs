namespace VirtualHoftalon_Server.Exceptions;

public class NotFoundLoginEntityException : Exception

{

    public NotFoundLoginEntityException(string msg)
    :base(msg)
    {
        
    }
}