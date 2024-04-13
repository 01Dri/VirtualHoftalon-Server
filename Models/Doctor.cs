using System.ComponentModel.DataAnnotations;

namespace VirtualHoftalon_Server.Models;

public class Doctor
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "O campo Name é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O campo Name deve ter no máximo 100 caracteres.")]
    public string Name { get; set; }

    public int? SectorId { get; set; }
    public Sector Sector { get; set;}
}