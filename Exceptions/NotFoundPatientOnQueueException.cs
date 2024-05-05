namespace VirtualHoftalon_Server.Exceptions;

public class NotFoundPatientOnQueueException : Exception
{

    public NotFoundPatientOnQueueException(string msg)
    :base(msg)
    {
        
    }
    
}