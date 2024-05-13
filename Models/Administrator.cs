using System.ComponentModel.DataAnnotations;
using VirtualHoftalon_Server.Enums;

namespace VirtualHoftalon_Server.Models;

public class Administrator
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo FirstName é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O campo FirstName deve ter no máximo 100 caracteres.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "O campo LastName é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O campo LastName deve ter no máximo 100 caracteres.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "O campo Cpf é obrigatório.")]
    [StringLength(14, MinimumLength = 14, ErrorMessage = "O campo Cpf deve ter 11 caracteres.")]
    public string Cpf { get; set; }

    [Required(ErrorMessage = "O campo Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "O campo Email deve ser um endereço de email válido.")]
    [MaxLength(100, ErrorMessage = "O campo Email deve ter no máximo 100 caracteres.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo DateBirth é obrigatório.")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime DateBirth { get; set; }

    public Roles Role = Roles.ADMIN;
    public int? LoginId { get; set; }
    public Login Login { get; set; }

}


    
    