using System.ComponentModel.DataAnnotations;

namespace VirtualHoftalon_Server.Models.Dto.Administrator;

public record AdministratorRequestDTO(
    
    [Required(ErrorMessage = "O campo FirstName é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O campo FirstName deve ter no máximo 100 caracteres.")]
    string FirstName,
    [Required(ErrorMessage = "O campo LastName é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O campo LastName deve ter no máximo 100 caracteres.")]
    string LastName,
    [Required(ErrorMessage = "O campo Cpf é obrigatório.")]
    [StringLength(14, MinimumLength = 14, ErrorMessage = "O campo Cpf deve ter 11 caracteres.")]
    string Cpf,
    [Required(ErrorMessage = "O campo Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "O campo Email deve ser um endereço de email válido.")]
    [MaxLength(100, ErrorMessage = "O campo Email deve ter no máximo 100 caracteres.")]
    string Email,
    [Required(ErrorMessage = "O campo DateBirth é obrigatório.")]
    [DataType(DataType.Date)]
    string DateBirth,
    [Required]
    string Password
    );
    