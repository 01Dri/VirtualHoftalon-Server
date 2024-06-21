using System.ComponentModel.DataAnnotations;
using VirtualHoftalon_Server.Validates;

namespace VirtualHoftalon_Server.Models.Dto;

public record RegisterLoginDTO(
    [Required]
    string Username,
    [Required]
    string Cpf,
    [Required]
    string Password,
    [Required]
    string Role);