using VirtualHoftalon_Server.Models.Dto;

namespace VirtualHoftalon_Server.Services.Interfaces;

public interface ILoginService
{
    RegisterLoginResponseDTO Register(RegisterLoginDTO login);
    LoginResponseDTO Login(LoginDTO login);
}