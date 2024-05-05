namespace VirtualHoftalon_Server.Exceptions;

public class InvalidAppointmentIdException : Exception
{
    public InvalidAppointmentIdException(string msg)
        : base(msg)
    {
    }
}