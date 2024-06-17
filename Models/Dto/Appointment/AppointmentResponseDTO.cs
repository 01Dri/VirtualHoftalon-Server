namespace VirtualHoftalon_Server.Models.Dto.Appointment;

public record AppointmentResponseDTO
    (int? Id, string Name, 
        int? PatientId, int? DoctorId,
        string DoctorName,
        int? SectorId,
        string SectorName,
        DateTime? Timestamp, 
        string? Description);