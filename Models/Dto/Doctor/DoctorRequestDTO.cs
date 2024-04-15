using System.ComponentModel.DataAnnotations;

namespace VirtualHoftalon_Server.Models.Dto;

public record DoctorRequestDTO(
    [Required(ErrorMessage = "O campo Name é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O campo Name deve ter no máximo 100 caracteres.")]
    string Name);
