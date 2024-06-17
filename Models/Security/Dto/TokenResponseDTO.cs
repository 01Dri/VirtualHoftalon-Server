namespace VirtualHoftalon_Server.Models.Security.Dto;

public record TokenResponseDTO(string accessToken, DateTime? expireAt, string refreshToken)
{
}