using VirtualHoftalon_Server.Enums;
using VirtualHoftalon_Server.Exceptions;
using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Models.Dto;
using VirtualHoftalon_Server.Pattern;
using VirtualHoftalon_Server.Repositories.Interfaces;
using VirtualHoftalon_Server.Security;
using VirtualHoftalon_Server.Security.interfaces;
using VirtualHoftalon_Server.Services.Interfaces;

namespace VirtualHoftalon_Server.Services;

public class LoginService : ILoginService
{

    private readonly ILoginRepository _repository;
    private readonly IPasswordEncrypter _passwordEncrypter;
    private readonly IRoleStrategyValidator _roleStrategyValidator;

    public LoginService(ILoginRepository repository, IPasswordEncrypter passwordEncrypter, IRoleStrategyValidator roleStrategyValidator)
    {
        _repository = repository;
        _passwordEncrypter = passwordEncrypter;
        _roleStrategyValidator = roleStrategyValidator;
    }

    public RegisterLoginResponseDTO Register(RegisterLoginDTO login)
    {
        Roles role = (Roles)Enum.Parse(typeof(Roles), login.Role);
        // Salva um login e faz o relacionamento entre a tabela Patient ou Doctor dependendo da strategy selecionada.
        Login loginEntity = _roleStrategyValidator.SaveLoginByRole(role, login);
        return new RegisterLoginResponseDTO(loginEntity.Username, loginEntity.Role.ToString());
    }

    public LoginResponseDTO Login(LoginDTO login)
    {
        string password = login.Password;
        Login loginEntity = _repository.GetLoginByUsername(login.Username) ??
                            throw new NotFoundLoginEntityException("Not found login by username");
        string decryptedPassword = _passwordEncrypter.Decrypt(loginEntity.Password);
        if (password == decryptedPassword)
        {
            return new LoginResponseDTO(loginEntity.Username, loginEntity.Role.ToString(),
                TokenService.GenerateToken(loginEntity));
        }
        throw new InvalidLoginException("Invalid login!");
    }
}