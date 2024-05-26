namespace VirtualHoftalon_Server.Exceptions;

public class FailedToSaveLoginException : Exception
{
    public FailedToSaveLoginException(string msg)
    :base(msg)
    {
    }
}