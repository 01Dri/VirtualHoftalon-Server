namespace VirtualHoftalon_Server.Models.Dto.Appointment;

public record AppointmentUpdateRequestDTO(
    int? Day, int? Month, int? Year, string? Hour ,int? SectorId);