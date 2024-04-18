namespace VirtualHoftalon_Server.Models.Dto.Appointment;

public record AppointmentListDTO(int? Id, int? PatientId, int? DoctorId, int? SectorId, DateTime? Timestamp);
