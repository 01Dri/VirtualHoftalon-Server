using System.ComponentModel.DataAnnotations;

namespace VirtualHoftalon_Server.Models.Dto.User;

public record UserLoginDTO(
    [Required(ErrorMessage = "Username is required")]
    string Username,
    [Required(ErrorMessage = "Password is required")]
    string Password);