namespace VirtualHoftalon_Server.Validates.Interface;

public interface IValidateRg
{
    public string RemoveIsNotNumeric(string txt);
    public bool ValidationRg(string rg);
}