namespace VirtualHoftalon_Server.Exceptions;

public class InvalidArgumentsUpdateSectorException : Exception
{
    public InvalidArgumentsUpdateSectorException(string message)
        :base(message)
    {}
}