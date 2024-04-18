using VirtualHoftalon_Server.Models;

namespace VirtualHoftalon_Server.Pattern;

public interface IAppointmentBuilder
{
    IAppointmentBuilder WithId(int id);
    IAppointmentBuilder WithName(string? name);
    IAppointmentBuilder WithPatient(Patient patient);
    IAppointmentBuilder WithDoctor(Doctor doctor);
    IAppointmentBuilder WithSector(Sector sector);
    IAppointmentBuilder WithTimestamp(DateTime time);
    Appointment Build();


}