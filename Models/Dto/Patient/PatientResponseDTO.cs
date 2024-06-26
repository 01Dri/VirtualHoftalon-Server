using VirtualHoftalon_Server.Enums;
using VirtualHoftalon_Server.Models.Dto.Appointment;

namespace VirtualHoftalon_Server.Models.Dto.Patient;

public record PatientResponseDTO(int? Id, string Name,
    string Phone, string Cpf,
    string Rg, string Email,
    DateTime BirthDate ,ClassificationPatient Classification, ICollection<AppointmentListDTO> Appointments);