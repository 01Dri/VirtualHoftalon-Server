using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualHoftalon_Server.Models;

public class Doctor
{
    public int? Id { get; set; }
    
    [Required(ErrorMessage = "O campo Name é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O campo Name deve ter no máximo 100 caracteres.")]
    public string? Name { get; set; }
    
    public virtual List<Appointment>? Appointments { get; set; }

    public Doctor()
    {
        
    }

    public Doctor(int? id, string? name)
    {
        Id = id;
        Name = name;
    }
}