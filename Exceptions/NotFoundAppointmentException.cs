namespace VirtualHoftalon_Server.Exceptions;

public class NotFoundAppointmentException : Exception
{
    public NotFoundAppointmentException(string message)
        :base(message)
    {}
    
}