using VirtualHoftalon_Server.Models.Dto.User;
using VirtualHoftalon_Server.Models.Security.Dto;

namespace VirtualHoftalon_Server.Services.Interfaces;

public interface IUserService
{
    UserRegisterResponseDTO Register(UserRegisterDTO userRegisterDto);
    TokenResponseDTO Login(UserLoginDTO userLoginDto);
}