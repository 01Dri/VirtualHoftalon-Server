namespace VirtualHoftalon_Server.Models.Dto;

public record LoginResponseDTO(

    int id,
    string Username,
    string Role,
    string Token);