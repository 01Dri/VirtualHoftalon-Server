using VirtualHoftalon_Server.Enums;
using VirtualHoftalon_Server.Exceptions;
using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Models.Dto;
using VirtualHoftalon_Server.Repositories.Interfaces;
using VirtualHoftalon_Server.Security.interfaces;

namespace VirtualHoftalon_Server.Pattern;

public class RolePatientStrategy : IRoleStrategy
{
    private readonly IPatientRepository _patientRepository;
    private readonly ILoginRepository _repository;
    private readonly IPasswordEncrypter _passwordEncrypter;

    public RolePatientStrategy(IPatientRepository patientRepository, ILoginRepository repository, IPasswordEncrypter passwordEncrypter)
    {
        _patientRepository = patientRepository;
        _repository = repository;
        _passwordEncrypter = passwordEncrypter;
    }


    public Login Save(Roles role, RegisterLoginDTO login)
    {
        Login loginEntity = new Login();
        

        Patient patientByCPF = _patientRepository.GetPatientByCpf(login.Cpf) ??
                               throw new NotFoundPatientException("Not found patient by CPF");

        loginEntity = new Login()
        {
            Username = login.Username,
            Password = _passwordEncrypter.Encrypt(login.Password),
            Role = role
        };

        _repository.Save(loginEntity);
        // Setando o relacionamento com a tabela Logins e Patients
        patientByCPF.Login = loginEntity;
        _patientRepository.UpdatePatient(patientByCPF);

        return loginEntity;
    }
}