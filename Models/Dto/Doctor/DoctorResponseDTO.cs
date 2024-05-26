using VirtualHoftalon_Server.Models.Dto.Appointment;

namespace VirtualHoftalon_Server.Models.Dto;

public record DoctorResponseDTO(
    int? Id,
    string? Cpf,
    string? Name,
    List<AppointmentResponseDTO>? appointments);