using System.ComponentModel.DataAnnotations;

namespace VirtualHoftalon_Server.Models.Dto.Appointment;

public record AppointmentDateFormatDTO(
    [Required(ErrorMessage = "O campo Day não pode ser nulo")]
    int? Day,
    [Required(ErrorMessage = "O campo Month não pode ser nulo")]
    int? Month,
    [Required(ErrorMessage = "O campo Year não pode ser nulo")]
    int? Year,
    [Required(ErrorMessage = "O campo Hour não pode ser nulo")]
    [RegularExpression(@"^(00:00|([01]\d|2[0-3]):[0-5]\d)$",
        ErrorMessage = "A hora deve estar no formato HH:MM, de 01:00 até 23:59 ou 00:00.")]
    string Hour);