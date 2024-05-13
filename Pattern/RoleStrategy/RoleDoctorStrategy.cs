using VirtualHoftalon_Server.Enums;
using VirtualHoftalon_Server.Exceptions;
using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Models.Dto;
using VirtualHoftalon_Server.Repositories.Interfaces;
using VirtualHoftalon_Server.Security.interfaces;

namespace VirtualHoftalon_Server.Pattern;

public class RoleDoctorStrategy : IRoleStrategy
{
    
    private readonly IDoctorRepository _doctorRepository;
    private readonly IPasswordEncrypter _passwordEncrypter;
    private readonly ILoginRepository _repository;

    public RoleDoctorStrategy(IDoctorRepository doctorRepository, IPasswordEncrypter passwordEncrypter, ILoginRepository repository)
    {
        _doctorRepository = doctorRepository;
        _passwordEncrypter = passwordEncrypter;
        _repository = repository;
    }

    public Login Save(Roles role, RegisterLoginDTO login)
    {
        Login loginEntity = new Login();

        Doctor doctorByCPF = _doctorRepository.GetDoctorByCPF(login.Cpf) ??
                             throw new NotFoundDoctorException("Not found doctor by CPF");

        loginEntity = new Login()
        {
            Username = login.Username,
            Password = _passwordEncrypter.Encrypt(login.Password),
            Role = role
        };

        _repository.Save(loginEntity);


        // Setando o relacionamento com a tabela Logins e Doctors

        doctorByCPF.Login = loginEntity;
        _doctorRepository.UpdateDoctor(doctorByCPF);

        return loginEntity;
    }
}