namespace VirtualHoftalon_Server.Models.Dto;

public record LoginResponseDTO(
    string Username,
    string Role,
    string Token);