namespace VirtualHoftalon_Server.Exceptions;

public class EmptyListPatientQueuesException : Exception
{
    public EmptyListPatientQueuesException(string msg)
    :base(msg)
    {
        
    }
    
}