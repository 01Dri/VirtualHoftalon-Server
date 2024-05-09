using VirtualHoftalon_Server.Exceptions;
using VirtualHoftalon_Server.Models.Dto.User;
using VirtualHoftalon_Server.Models.Security;
using VirtualHoftalon_Server.Models.Security.Dto;
using VirtualHoftalon_Server.Repositories.Interfaces;
using VirtualHoftalon_Server.Security;
using VirtualHoftalon_Server.Security.interfaces;
using VirtualHoftalon_Server.Services.Interfaces;

namespace VirtualHoftalon_Server.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordEncrypter _passwordEncrypter;

    public UserService(IUserRepository userRepository, IPasswordEncrypter passwordEncrypter)
    {
        _userRepository = userRepository;
        _passwordEncrypter = passwordEncrypter;
    }

    public UserRegisterResponseDTO Register(UserRegisterDTO userRegisterDto)
    {
        User user = new User()
        {
            Id = null,
            Username = userRegisterDto.Username,
            Password = _passwordEncrypter.Encrypt(userRegisterDto.Password),
            Role = userRegisterDto.Role
        };
        _userRepository.SaveUser(user);
        return new UserRegisterResponseDTO(user.Username, user.Role);
    }

    public TokenResponseDTO Login(UserLoginDTO userLoginDto)
    {

        string passowrd = userLoginDto.Password;
        User userByUsername = _userRepository.GetUserByUsername(userLoginDto.Username) ?? throw new NotFoundUserException("Not found user!");
        string descryptedPassword = _passwordEncrypter.Decrypt(userByUsername.Password);
        if (passowrd == descryptedPassword)
        {
            return new TokenResponseDTO(TokenService.GenerateToken(userByUsername));
        }

        throw new InvalidLoginException("Invalid login!");
    }
}