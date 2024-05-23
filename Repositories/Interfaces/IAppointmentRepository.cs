using VirtualHoftalon_Server.Models;

namespace VirtualHoftalon_Server.Repositories.Interfaces;

public interface IAppointmentRepository
{
    
    Appointment? GetAppointmentById(int id);

    Appointment SaveAppointment(Appointment Appointment);

    IEnumerable<Appointment> GetAll();
    
    Appointment UpdateAppointment(Appointment AppointmentById);
    void Delete(Appointment AppointmentById);
    
    
}