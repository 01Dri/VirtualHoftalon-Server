using VirtualHoftalon_Server.Models.Dto;
using VirtualHoftalon_Server.Models.Security.Dto;

namespace VirtualHoftalon_Server.Services.Interfaces;

public interface ILoginService
{
    RegisterLoginResponseDTO Register(RegisterLoginDTO login);
    LoginResponseDTO Login(LoginDTO login);
    
    LoginResponseDTO RefreshToken(RefreshTokenRequestDTO refreshTokenRequestDto);
}