using VirtualHoftalon_Server.Models.Security.Dto;

namespace VirtualHoftalon_Server.Models.Dto;

public record LoginResponseDTO(

    int id,
    string Username,
    string Role,
    TokenResponseDTO TokenResponseDto);