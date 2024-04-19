using VirtualHoftalon_Server.Models.Dto.Appointment;

namespace VirtualHoftalon_Server.Services.Interfaces;

public interface IAppointmentService
{
    List<AppointmentResponseDTO> GetAll();
    AppointmentResponseDTO SaveAppointment(AppointmentRequestDTO AppointmentRequestDto);
    AppointmentResponseDTO GetOneById(int id);
    AppointmentResponseDTO UpdateAppointmentById(int id, AppointmentUpdateRequestDTO appointment);
    bool DeleteAppointmentById(int id);

    
}