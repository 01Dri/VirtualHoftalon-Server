namespace VirtualHoftalon_Server.Models.Dto.Appointment;

public record AppointmentResponseDTO(string Name, int? Id, int? PatientId, int? DoctorId, int? SectorId, DateTime? Timestamp);