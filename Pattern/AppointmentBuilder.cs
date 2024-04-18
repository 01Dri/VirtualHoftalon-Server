using VirtualHoftalon_Server.Models;

namespace VirtualHoftalon_Server.Pattern;

public class AppointmentBuilder : IAppointmentBuilder
{
    private int? Id;
    private string? Name;
    private Patient? Patient;
    private Doctor? Doctor;
    private Sector? Sector;
    private DateTime? Time;
    
    
    public IAppointmentBuilder WithId(int id)
    {
        this.Id = id;
        return this;
    }

    public IAppointmentBuilder WithName(string name)
    {
        this.Name = name;
        return this;
    }

    public IAppointmentBuilder WithPatient(Patient patient)
    {
        this.Patient = patient;
        return this;
    }

    public IAppointmentBuilder WithDoctor(Doctor doctor)
    {
        this.Doctor = doctor;
        return this;

    }

    public IAppointmentBuilder WithSector(Sector sector)
    {
        this.Sector = sector;
        return this;
    }

    public IAppointmentBuilder WithTimestamp(DateTime time)
    {
        this.Time = time;
        return this;
    }

    public Appointment Build()
    {
        return new Appointment(this.Id,this.Name, this.Patient.Id,
            this.Patient, this.Doctor.Id,
            this.Doctor, this.Sector.Id,
            this.Sector, this.Time);
    }
}