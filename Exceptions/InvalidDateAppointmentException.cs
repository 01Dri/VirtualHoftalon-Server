namespace VirtualHoftalon_Server.Exceptions;

public class InvalidDateAppointmentException : Exception
{
    public InvalidDateAppointmentException(string message)
        :base(message)
    {}
}