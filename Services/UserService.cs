using VirtualHoftalon_Server.Models.Dto.User;
using VirtualHoftalon_Server.Models.Security;
using VirtualHoftalon_Server.Repositories.Interfaces;
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

    public string Login(UserLoginDTO userLoginDto)
    {
        throw new NotImplementedException();
    }
}