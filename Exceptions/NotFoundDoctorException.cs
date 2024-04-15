namespace VirtualHoftalon_Server.Exceptions;

public class NotFoundDoctorException : Exception
{
    public NotFoundDoctorException(string message)
        :base(message)
    {}
}