namespace VirtualHoftalon_Server.Exceptions;

public class FailedToSavePDFException : Exception
{
    public FailedToSavePDFException(string msg)
    :base(msg)
    {
        
    }
    
}