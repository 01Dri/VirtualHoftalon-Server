using System.ComponentModel.DataAnnotations;

namespace VirtualHoftalon_Server.Models.Dto.Appointment;

public record AppointmentRequestDTO(
    AppointmentDateFormatDTO DateFormat,
    [Required(ErrorMessage = "O Nome não pode ser nulo")]
    string? Name,
    [Required(ErrorMessage = "O ID do Paciente não pode ser nulo!")]
    int? PatientId,
    [Required(ErrorMessage = "O ID do Setor não pode ser nulo!")]
    int? SectorId,
    string? Description);
    

