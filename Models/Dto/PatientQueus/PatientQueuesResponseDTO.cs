namespace VirtualHoftalon_Server.Models.Dto.PatientQueus;

public record PatientQueuesResponseDTO(
    int? Id,
    int? PatientId,
    int Position,
    string AppointmentName,
    string AppointmentHour,
    string PatientName,
    string Password,
    bool Preferencial
    );