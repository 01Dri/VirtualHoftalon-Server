using VirtualHoftalon_Server.Models.Dto.User;

namespace VirtualHoftalon_Server.Services.Interfaces;

public interface IUserService
{
    UserRegisterResponseDTO Register(UserRegisterDTO userRegisterDto);
    string Login(UserLoginDTO userLoginDto);
}