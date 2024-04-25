namespace VirtualHoftalon_Server.Exceptions;

public class FailedToSetAppointmentOnPDFGeneratorException : Exception
{
    public FailedToSetAppointmentOnPDFGeneratorException(string msg)
        :base(msg)
    {

    }

}