using System.ComponentModel.DataAnnotations;

namespace VirtualHoftalon_Server.Models.Dto;

public record DoctorRequestDTO(
    [Required(ErrorMessage = "O campo Name é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O campo Name deve ter no máximo 100 caracteres.")]
    string Name,
    [Required(ErrorMessage = "O campo ID é obrigatório.")]
    [RegularExpression(@"^\d+$", ErrorMessage = "O campo ID deve conter apenas números inteiros.")]
    int SectorId);