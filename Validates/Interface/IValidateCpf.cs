namespace VirtualHoftalon_Server.Validates.Interface;

public interface IValidateCpf
{
    public string RemoveIsNotNumeric(string txt);

    public bool ValidationCpf(string cpf);
}