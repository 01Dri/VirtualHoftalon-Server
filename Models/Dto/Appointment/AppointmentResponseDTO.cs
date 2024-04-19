namespace VirtualHoftalon_Server.Models.Dto.Appointment;

public record AppointmentResponseDTO(int? Id, string Name,  int? PatientId, int? DoctorId, int? SectorId, DateTime? Timestamp);