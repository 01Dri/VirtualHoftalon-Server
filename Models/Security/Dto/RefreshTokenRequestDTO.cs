namespace VirtualHoftalon_Server.Models.Security.Dto;

public record RefreshTokenRequestDTO(string refreshToken, int? loginId);