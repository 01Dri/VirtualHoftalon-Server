using System.ComponentModel.DataAnnotations;

namespace VirtualHoftalon_Server.Models.Dto;

public record LoginDTO(
    [Required]
    string Username,
    [Required]
    string Password);