using System.ComponentModel.DataAnnotations;

namespace VirtualHoftalon_Server.Models.Dto.Sector;

public record SectorRequestDTO
    (
        [Required(ErrorMessage = "O campo Name é obrigatório.")]
        string Name,
        [Required(ErrorMessage = "O campo RoomNumber é obrigatório.")]
        int RoomNumber,
        [Required(ErrorMessage = "O campo DoctorId é obrigatório.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "O campo ID deve conter apenas números inteiros.")]
        int DoctorId,
        [Required(ErrorMessage = "O campo TagId é obrigatório.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "O campo ID deve conter apenas números inteiros.")]
        int? TagId);