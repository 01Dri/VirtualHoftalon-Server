using VirtualHoftalon_Server.Models;

namespace VirtualHoftalon_Server.Pattern;

public class AppointmentBuilder : IAppointmentBuilder
{
    private int? Id;
    private string? Name;
    private Patient? Patient;
    private Doctor? Doctor;
    private Sector? Sector;
    private int? Day;
    private int? Month;
    private int? Year;
    private string? Hour;
    private string? Description;
    
    
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

    public IAppointmentBuilder WithDay(int? day)
    {
        this.Day = day;
        return this;
    }

    public IAppointmentBuilder WithMonth(int? month)
    {
        this.Month = month;
        return this;
    }

    public IAppointmentBuilder WithYear(int? year)
    {
        this.Year = year;
        return this;
    }

    public IAppointmentBuilder WithHour(string? hour)
    {
        this.Hour = hour;
        return this;

    }

    public IAppointmentBuilder WithDescription(string? description)
    {
        this.Description = description;
        return this;
    }


    public Appointment Build()
    {
        return new Appointment(this.Id,this.Name, this.Patient.Id,
            this.Patient, this.Doctor.Id,
            this.Doctor, this.Sector.Id,
            this.Sector, this.Day, this.Month, this.Year, this.Hour, this.Description);
    }
}