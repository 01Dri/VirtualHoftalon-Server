namespace VirtualHoftalon_Server.Exceptions;

public class NotFoundPatientException : Exception
{
    public NotFoundPatientException(string message)
        :base(message)
    {
        
    }

}