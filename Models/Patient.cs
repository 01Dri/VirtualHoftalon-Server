using System.ComponentModel.DataAnnotations;
using VirtualHoftalon_Server.Enums;

namespace VirtualHoftalon_Server.Models;

public class Patient
{
    
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo Name é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O campo Name deve ter no máximo 100 caracteres.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O campo PhoneNumber é obrigatório.")]
    [MaxLength(20, ErrorMessage = "O campo PhoneNumber deve ter no máximo 20 caracteres.")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "O campo Cpf é obrigatório.")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "O campo Cpf deve ter 11 caracteres.")]
    public string Cpf { get; set; }

    [Required(ErrorMessage = "O campo Rg é obrigatório.")]
    [MaxLength(20, ErrorMessage = "O campo Rg deve ter no máximo 20 caracteres.")]
    public string Rg { get; set; }

    [Required(ErrorMessage = "O campo Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "O campo Email deve ser um endereço de email válido.")]
    [MaxLength(100, ErrorMessage = "O campo Email deve ter no máximo 100 caracteres.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo DateBirth é obrigatório.")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime DateBirth { get; set; }

    [Required(ErrorMessage = "O campo ClassificationPatient é obrigatório.")]
    public ClassificationPatient ClassificationPatient { get; set; }
    
    public ICollection<SectorPatient> SectorPatients { get; set; }

}

