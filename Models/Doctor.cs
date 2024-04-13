using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualHoftalon_Server.Models;

public class Doctor
{
    public int? Id { get; set; }
    
    [Required(ErrorMessage = "O campo Name é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O campo Name deve ter no máximo 100 caracteres.")]
    public string? Name { get; set; }
    [ForeignKey("Sector")]
    public int? SectorId { get; set; }

    public Sector Sector { get; set; }

    public Doctor(int? id, string? name, int? sectorId)
    {
        Id = id;
        Name = name;
        SectorId = sectorId;
    }

}